using EventShared;
using MassTransit;

namespace JobService.API.Consumers
{
    public class CompanyDontHaveJobCountEventConsumer : IConsumer<CompanyDontHaveJobCountEvent>
    {
        public async Task Consume(ConsumeContext<CompanyDontHaveJobCountEvent> context)
        {
            await Task.CompletedTask;
            throw new Exception("Bu Firmanın İLan Yaynlama Hakkı Kalmamıştır.");

        }
    }
}
