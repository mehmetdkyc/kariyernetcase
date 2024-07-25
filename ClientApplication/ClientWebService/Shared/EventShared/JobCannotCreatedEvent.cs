
using EventShared.Common;

namespace ClientService.Infrastructure.Events
{
    public class JobCannotCreatedEvent : IEvent
    {
        public Guid CompanyId { get; set; }
    }
}
