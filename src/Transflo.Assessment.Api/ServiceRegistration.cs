using Transflo.Assessment.Core;
using Transflo.Assessment.Infrastructure;

namespace Transflo.Assessment.Api
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddInfrastructureLayer();
            services.AddCoreLayer();
            return services;
        }

    }
}
