using EventShared.Common;

namespace EventShared
{
    public class JobCannotCreatedEvent:IEvent
    {
        public Guid CompanyId { get; set; }
    }
}
