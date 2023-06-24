using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using System.Reflection;

namespace Transflo.Assessment.Core
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddFluentValidation(new[] { Assembly.GetExecutingAssembly() });
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            return services;
        }
    }
}