using AutoMapper;
using ClientService.Application.Dtos.Company;
using ClientService.Domain.Entities;

namespace ClientService.Application.Mappings
{
    public class CompanyMappings:Profile
    {
        public CompanyMappings()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Company, CreateCompanyDto>().ReverseMap();
            CreateMap<UpdateCompanyDto, CompanyDto>().ReverseMap();
            CreateMap<UpdateCompanyDto, Company>().ReverseMap();
        }
    }
}
