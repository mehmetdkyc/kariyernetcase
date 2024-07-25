using AutoMapper;
using ClientService.Application.Dtos.Company;
using ClientService.Application.Interfaces.Services;
using ClientService.Infrastructure;
using EventShared;
using MassTransit;

namespace ClientService.API.Consumers
{

    public class CanCompanyShareJobEventConsumer : IConsumer<CanCompanyShareJobEvent>
    {
        readonly ISendEndpointProvider _sendEndpointProvider;
        readonly IPublishEndpoint _publishEndpoint;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CanCompanyShareJobEventConsumer(ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint, ICompanyService companyService, IMapper mapper)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
            _companyService = companyService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CanCompanyShareJobEvent> context)
        {
            var countShareJob = await _companyService.GetById(context.Message.CompanyId);
            if (countShareJob !=null && countShareJob.JobCount>0)
            {
                //İlan Hakkını 1 azaltıyoruz.
                countShareJob.JobCount -= 1;
                var dtoMapped = _mapper.Map<UpdateCompanyDto>(countShareJob);
                await _companyService.UpdateCompanyJobCount(dtoMapped);

                CompanyCanShareJobEvent companyCanShareJobEvent = new(){CanShare=true,
                Benefits=context.Message.Benefits,
                CompanyId=context.Message.CompanyId,
                JobDescription= context.Message.JobDescription,
                Role=context.Message.Role,
                Salary=context.Message.Salary,
                WorkType=context.Message.WorkType};

                ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.CompanyCanShareJobEvent}"));
                await sendEndpoint.Send(companyCanShareJobEvent);
            }
            else
            {
                //İlan Hakkı Yoksa
                CompanyDontHaveJobCountEvent companyDontHaveJobCountEvent = new()
                {
                    CompanyId= context.Message.CompanyId
                };

                await _publishEndpoint.Publish(companyDontHaveJobCountEvent);
            }
        }
    }
}
