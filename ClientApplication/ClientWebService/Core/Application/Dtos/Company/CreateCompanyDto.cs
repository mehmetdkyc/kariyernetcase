namespace ClientService.Application.Dtos.Company
{
    public class CreateCompanyDto
    {
        public required string CompanyName { get; set; }
        public required string MobilePhone { get; set; }
        public required string Adres { get; set; }
    }
}
