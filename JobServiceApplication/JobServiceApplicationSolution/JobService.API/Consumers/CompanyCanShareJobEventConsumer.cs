using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Dtos;
using EventShared;
using MassTransit;

namespace JobService.API.Consumers
{
    public class CompanyCanShareJobEventConsumer : IConsumer<CompanyCanShareJobEvent>
    {       
        readonly IPublishEndpoint _publishEndpoint;
        private readonly IJobService _jobService;
        public CompanyCanShareJobEventConsumer( IPublishEndpoint publishEndpoint, IJobService jobService)
        {
            _publishEndpoint = publishEndpoint;
            _jobService = jobService;
        }

        public async Task Consume(ConsumeContext<CompanyCanShareJobEvent> context)
        {
            try
            {
                if (!context.Message.CanShare) throw new Exception($"Firmanın İlan Paylaşma Hakkı Kalmamıştır.");

                JobInsertDto insertDto = new()
                {
                    JobDescription = context.Message.JobDescription,
                    Role=context.Message.Role,
                    Salary= context.Message.Salary,
                    Benefits= context.Message.Benefits,
                    CompanyId= context.Message.CompanyId,
                    WorkType = context.Message.WorkType
                };
                #region İlan Kalitesi Hesaplama
                int jobQuality = 0;
                if (!string.IsNullOrEmpty(insertDto.WorkType)) jobQuality += 1;
                if (insertDto.Salary != 0) jobQuality += 1;
                if (!string.IsNullOrEmpty(insertDto.Benefits)) jobQuality += 1;
                #endregion


                insertDto.JobQuality = jobQuality;
                var list = await _jobService.CreateAsync(insertDto);
            }
            catch (Exception)
            {
                //İlan oluşturulurken bir sorun meydana gelirse JobCountu tekrardan arttırmak gerekecektir.
                JobCannotCreatedEvent jobCannotCreatedEvent = new()
                {
                    CompanyId = context.Message.CompanyId
                };

                await _publishEndpoint.Publish(jobCannotCreatedEvent);
            }
        }
    }
}
