using BusinessLayer.Abstract;
using BusinessLayer.Mappings;
using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using DataAccess;
using BusinessLayer.Validators.JobValidator;
using FluentValidation;

namespace BusinessLayer
{
    public static class ServiceRegistration
    {
        public static void AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(JobMapping));
            services.AddScoped<IJobService, JobService>();
            services.AddDataAccessLayerServices();
            services.AddValidatorsFromAssemblyContaining<CreateJobDtoValidator>();
        }
    }
}
