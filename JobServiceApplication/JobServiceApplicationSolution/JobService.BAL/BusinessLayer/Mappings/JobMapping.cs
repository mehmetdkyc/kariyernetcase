using AutoMapper;
using BusinessLayer.Dtos;
using DataAccess.Entities;

namespace BusinessLayer.Mappings
{
    public class JobMapping:Profile
    {
        public JobMapping()
        {
            CreateMap<Job, JobInsertResponseDto>().ReverseMap();
            CreateMap<Job, JobInsertDto>().ReverseMap();
            CreateMap<Job, JobDto>().ReverseMap();
        }
    }
}
