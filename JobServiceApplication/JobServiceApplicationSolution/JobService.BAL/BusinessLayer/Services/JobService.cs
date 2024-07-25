using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Dtos;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;

namespace BusinessLayer.Services
{
    public class JobService : IJobService
    {
        private readonly IClientRepository<Job> _repository;
        private readonly IMapper _mapper;
        private readonly string indexName ="jobs";
        public JobService(IClientRepository<Job> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<JobInsertResponseDto> CreateAsync(JobInsertDto entity)
        {
            var expiredDate = DateTime.Now.AddDays(15);
            var mappedEntity = _mapper.Map<Job>(entity);
            mappedEntity.CreatedDate= DateTime.Now;
            mappedEntity.ExpiredDate = new DateTime(expiredDate.Year, expiredDate.Month, expiredDate.Day);
            var response = await _repository.CreateAsync(mappedEntity, indexName);
            if (!response.IsValidResponse) throw new Exception($"{typeof(Job)} not added!");

            mappedEntity.Id = response.Id;
            return _mapper.Map<JobInsertResponseDto>(mappedEntity);
        }

        public async Task<List<JobDto>> GetAllAsync()
        {
            var response = await _repository.GetAllAsync(indexName);
            return _mapper.Map<List<JobDto>>(response);
        }

        public async Task<List<JobDto>> GetJobsByExpiredDateAsync(string expiredDate)
        {
            var response = await _repository.GetAllByExpiredDateAsync(indexName,expiredDate);
            return _mapper.Map<List<JobDto>>(response);
        }
    }
}
