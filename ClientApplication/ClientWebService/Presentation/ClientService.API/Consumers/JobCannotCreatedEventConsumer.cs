using AutoMapper;
using ClientService.Application.Dtos.Company;
using ClientService.Application.Interfaces.Services;
using ClientService.Infrastructure.Events;
using MassTransit;

namespace ClientService.API.Consumers
{
    public class JobCannotCreatedEventConsumer : IConsumer<JobCannotCreatedEvent>
    {

        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public JobCannotCreatedEventConsumer(ICompanyService companyService, IMapper mapper)
        {

            _companyService = companyService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<JobCannotCreatedEvent> context)
        {
            var countShareJob = await _companyService.GetById(context.Message.CompanyId);

            countShareJob.JobCount += 1;
            var dtoMapped = _mapper.Map<UpdateCompanyDto>(countShareJob);
            await _companyService.UpdateCompanyJobCount(dtoMapped);
        }
    }
}
