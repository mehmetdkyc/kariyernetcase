using Castle.Core.Configuration;
using ClientService.Application;
using ClientService.Application.Interfaces.GenericService;
using ClientService.Application.Interfaces.Services;
using ClientService.Persistance.Context;
using ClientService.Persistance.Repositories;
using ClientService.Persistance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClientService.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services,string connectionString)
        {
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddApplicationServices();
            services.AddDbContext<ClientContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }
    }
}
