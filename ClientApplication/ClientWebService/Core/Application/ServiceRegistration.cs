using AutoMapper;
using ClientService.Application.Mappings;
using ClientService.Application.Validations.CompanyDtoValidator;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace ClientService.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CompanyMappings));
            services.AddScoped<IMapper, Mapper>();
            services.AddValidatorsFromAssemblyContaining<CreateCompanyDtoValidator>();
        }
    }
}
