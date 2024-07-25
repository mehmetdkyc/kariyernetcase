using ClientService.Application.Dtos.Company;
using ClientService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public ClientController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompany()
        {
            var response = await _companyService.GetAll();
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyDto insertDto)
        {
            var isExist = await _companyService.GetByMobilePhone(insertDto.MobilePhone);
            if (isExist) throw new Exception("Bu Telefon Numarasıyla Oluşturulmuş Firma Vardır.");

            var response = await _companyService.CreateCompany(insertDto);
            return Ok(response);
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> Delete(Guid companyId)
        {
            await _companyService.DeleteCompany(companyId);
            return NoContent();
        }
    }
}
