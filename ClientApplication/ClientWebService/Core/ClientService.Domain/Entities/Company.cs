using ClientService.Domain.Common;

namespace ClientService.Domain.Entities
{
    public sealed class Company:BaseEntity
    {
        public required string CompanyName { get; set; }
        public required string MobilePhone { get; set; }
        public required string Adres { get; set; }
        public required int JobCount { get; set; }
    }
}
