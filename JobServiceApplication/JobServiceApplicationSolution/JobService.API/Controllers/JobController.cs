using BusinessLayer.Abstract;
using BusinessLayer.Dtos;
using EventShared;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;

namespace JobService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IPublishEndpoint _publishEndpoint;

        public JobController(IJobService jobService, IPublishEndpoint publishEndpoint)
        {
            _jobService = jobService;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            var list = await _jobService.GetAllAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(JobInsertDto insertDto)
        {
            #region Rabbitmq ile İLanı Paylaşan Şirketin hakkı var mı diye kontrol etmeliyim.

            CanCompanyShareJobEvent jobevent = new()
            {
                Benefits = insertDto.Benefits,
                CompanyId = insertDto.CompanyId,
                JobDescription = insertDto.JobDescription,
                Role = insertDto.Role,
                Salary = insertDto.Salary,
                WorkType = insertDto.WorkType
            };
            await _publishEndpoint.Publish(jobevent);

            #endregion
            return Created();
        }

        [HttpGet("{expiredDate}")]
        public async Task<IActionResult> GetJobsByExpiredDate(string expiredDate)
        {
            var list = await _jobService.GetJobsByExpiredDateAsync(expiredDate);
            return Ok(list);
        }
    }
}
