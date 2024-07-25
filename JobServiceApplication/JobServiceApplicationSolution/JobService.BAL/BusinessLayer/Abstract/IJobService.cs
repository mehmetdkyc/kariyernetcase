using BusinessLayer.Dtos;

namespace BusinessLayer.Abstract
{
    public interface IJobService
    {
        Task<List<JobDto>> GetAllAsync();
        Task<List<JobDto>> GetJobsByExpiredDateAsync(string expiredDate);
        Task<JobInsertResponseDto> CreateAsync(JobInsertDto entity);
    }
}
