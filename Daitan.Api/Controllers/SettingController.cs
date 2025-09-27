using Daitan.Api.Controllers.Base;
using Daitan.Business.Repositories.Settings;
using Daitan.Business.Repositories.Users;
using Daitan.Data.Dto.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Daitan.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SettingController : BaseController
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IUserRepository _userRepository;
        public SettingController(ISettingRepository settingRepository, 
            IUserRepository userRepository)
        {
            _settingRepository = settingRepository;
            _userRepository = userRepository;
        }

        [HttpPost("SaveCompanySettings")]
        public async Task<IActionResult> SaveCompanySettings([FromBody] CompanyDto Dto)
        {
            Dto.UserId = CurrentUserId;
            await _userRepository.SaveCompanySettings(Dto);
            return ApiResponse(Dto.CompanyName);
        }

        [HttpPost("AddOrUpdateDepartment")]
        public async Task<ActionResult> AddDepartment([FromBody] DepartmentDto Dto)
        {
            var id = await _settingRepository.AddOrUpdateDepartment(Dto);
            return ApiResponse(id);
        }

        [HttpGet("EditDepartment")]
        public async Task<ActionResult> EditDepartment([FromQuery] string MachineId)
        {
            var id = await _settingRepository.EditMachine(MachineId);
            return ApiResponse(id);
        }

        [HttpGet("GetAllDepartment")]
        public async Task<ActionResult> GetAllDepartment()
        {
            var id = await _settingRepository.GetAllDepartments();
            return ApiResponse(id);
        }


        [HttpPost("AddOrUpdateMachine")]
        public async Task<IActionResult> AddMachine(MachineDto Dto)
        {
            var id = await _settingRepository.AddOrUpdateMachine(Dto);
            return ApiResponse(id);
        }

    }
}
