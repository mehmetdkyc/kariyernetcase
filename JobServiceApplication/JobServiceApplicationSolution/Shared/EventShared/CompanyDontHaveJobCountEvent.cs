using EventShared.Common;

namespace EventShared
{
    public class CompanyDontHaveJobCountEvent:IEvent
    {
        public Guid CompanyId { get; set; }
    }
}
