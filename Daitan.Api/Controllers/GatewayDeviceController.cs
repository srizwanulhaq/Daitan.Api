using Daitan.Business.Repositories.GatewayDevices;
using Daitan.Data.Dao.PagedData;
using Daitan.Data.Dto.GatewayDevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Daitan.Api.Controllers
{
   // [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class GatewayDeviceController : ControllerBase 
    {
        private readonly IGatewayDeviceRepository _repo;
        public GatewayDeviceController(IGatewayDeviceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        { 
            return Ok(_repo.GetAll());
        }

        [HttpGet("Edit")]
        public IActionResult Get([FromQuery] string Id)
        {
            return Ok(_repo.GetById(Id));
        }

        [HttpGet("AddOrUpdate")]
        public IActionResult AddOrUpdate([FromBody] GatewayDeviceDto Dto)
        {
            return Ok(_repo.AddOrUpdate(Dto));
        }


        [HttpPost("savereading")]
        public async Task<IActionResult> SaveReading([FromBody] ModbusTcpDeviceDto obj)
        {
            var res = await _repo.SaveDeviceTcpData(obj);
            return Ok(res);
        }

        [HttpPost("GetPagedData")]
        public IActionResult GetPagedData([FromBody] PagedRecordsParams Params)
        {
            return Ok(_repo.GetPagedRecords(Params));
        }

    }
}
