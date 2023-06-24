using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Transflo.Assessment.Core.Domain;

namespace Transflo.Assessment.Infrastructure.Persistence.Seeds
{
    public class DriverSeed
    {
        private readonly IServiceProvider _serviceProvider;

        public DriverSeed(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SeedDriversDataAsync()
        {
            using IServiceScope? scope = _serviceProvider.CreateScope();
            TransfloDBContext? context = scope.ServiceProvider.GetRequiredService<TransfloDBContext>();

            if (await context.Drivers.AnyAsync())
            {
                return;
            }

            Faker<Driver>? faker = new Faker<Driver>()
                .RuleFor(d => d.LastName, f => f.Person.LastName)
                .RuleFor(d => d.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(d => d.FirstName, f => f.Person.FirstName)
                .RuleFor(d => d.EmailAddress, f => f.Person.Email);

            List<Driver>? drivers = faker.Generate(100);

            await context.AddRangeAsync(drivers);
            await context.SaveChangesAsync();
        }
    }


}
