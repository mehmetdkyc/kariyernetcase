using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class ServiceRegistration
    {
        public static void AddDataAccessLayerServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IClientRepository<>), typeof(ClientRepository<>));

        }
    }
}
