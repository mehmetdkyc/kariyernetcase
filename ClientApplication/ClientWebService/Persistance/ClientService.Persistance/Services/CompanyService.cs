using AutoMapper;
using ClientService.Application.Dtos.Company;
using ClientService.Application.Interfaces.GenericService;
using ClientService.Application.Interfaces.Services;
using ClientService.Domain.Entities;

namespace ClientService.Persistance.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Company> _companyRepo;

        public CompanyService(IMapper mapper, IRepository<Company> companyRepo)
        {
            _mapper = mapper;
            _companyRepo = companyRepo;
        }

        public async Task<CompanyDto> CreateCompany(CreateCompanyDto insertDto)
        {
            var mappedData = _mapper.Map<Company>(insertDto);
            mappedData.JobCount = 2;
            mappedData.CreatedDate = DateTime.UtcNow;
            await _companyRepo.AddAsync(mappedData);
            return _mapper.Map<CompanyDto>(mappedData);
        }

        public async Task DeleteCompany(Guid companyId)
        {
            var entity = await _companyRepo.GetAsync(x=>x.Id==companyId);
            if (entity is null) throw new Exception($"{typeof(Company)} not found !");
            await _companyRepo.RemoveAsync(entity);
        }

        public async Task<List<CompanyDto>> GetAll()
        {
            var list = await _companyRepo.GetAllAsync(x=>x.Id!= null);
            return _mapper.Map<List<CompanyDto>>(list);
        }

        public async Task<CompanyDto> GetById(Guid id)
        {
            var entity = await _companyRepo.GetAsync(x => x.Id == id);
            return _mapper.Map<CompanyDto>(entity);
        }

        public async Task<bool> GetByMobilePhone(string mobilePhone)
        {
            var entity = await _companyRepo.GetAsync(x => x.MobilePhone == mobilePhone);
            return entity is not null;
        }

        public async Task UpdateCompanyJobCount(UpdateCompanyDto updateDto)
        {
            var data = await _companyRepo.GetAsync(x => x.Id == updateDto.Id);
            data.JobCount = updateDto.JobCount;
            data.UpdatedDate = DateTime.UtcNow;
            await _companyRepo.UpdateAsync(data);
        }
    }
}
