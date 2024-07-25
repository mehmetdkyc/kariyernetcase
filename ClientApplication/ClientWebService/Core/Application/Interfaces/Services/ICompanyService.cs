using ClientService.Application.Dtos.Company;

namespace ClientService.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto> CreateCompany(CreateCompanyDto insertDto);
        Task UpdateCompanyJobCount(UpdateCompanyDto updateDto);
        Task<bool> GetByMobilePhone(string mobilePhone);
        Task DeleteCompany(Guid companyId);
        Task<List<CompanyDto>> GetAll();
        Task<CompanyDto> GetById(Guid id);
    }
}
