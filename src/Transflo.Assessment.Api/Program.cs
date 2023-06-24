using Microsoft.AspNetCore.Mvc;
using Serilog;
using Transflo.Assessment.Api;
using Transflo.Assessment.Api.Filters;
using Transflo.Assessment.Api.Middlewares;
using Transflo.Assessment.Infrastructure.Persistence.Seeds;
using Transflo.Assessment.Shared.Models.Settings;


var builder = WebApplication.CreateBuilder(args);


var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



Setting.SetConfiguration(builder.Configuration);

builder.Services
    .AddServices()
    .AddControllers(options =>
    {
        options.Filters.Add<ValidateModelStateAttribute>();
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200") // or AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});


WebApplication? app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (IServiceScope? scope = app.Services.CreateScope())
    {
        DriverSeed? driverSeed = scope.ServiceProvider.GetRequiredService<DriverSeed>();
        await driverSeed.SeedDriversDataAsync();
    }

}

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

//enable COrs

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.UseAuthorization();

app.MapControllers();

app.Run();
