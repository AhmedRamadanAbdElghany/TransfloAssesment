using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Transflo.Assessment.Core.Interfaces.Repositories;
using Transflo.Assessment.Infrastructure.Persistence;
using Transflo.Assessment.Infrastructure.Persistence.Repositories;
using Transflo.Assessment.Infrastructure.Persistence.Seeds;
using Transflo.Assessment.Shared.Models.Settings;

namespace Transflo.Assessment.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddDbContext<TransfloDBContext>(options =>
            options.UseSqlServer(Setting.ApplicationConnectionString));
            services.RegisterServices();

            return services;
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<DriverSeed>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IReadonlyRepository<>), typeof(ReadonlyRepository<>));
        }
    }
}