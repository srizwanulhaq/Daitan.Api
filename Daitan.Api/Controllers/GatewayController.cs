using Daitan.Api.Controllers.Base;
using Daitan.Business.Repositories.Gateways;
using Daitan.Data.Dto.Gateways;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Daitan.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GatewayController : BaseController
    {

        private readonly IGatewayRepository _repo;
        public GatewayController(IGatewayRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("getall")]
        public IActionResult Get()
        {
            var data = _repo.GetAll();
            return Ok(data);
        }

        [HttpPost("addorupdate")]
        public IActionResult Add(GatewayDto Dto)
        {
            var data = _repo.AddOrUpdate(Dto);

            return Ok();
        }

        [HttpGet("edit")]
        public IActionResult Edit(string Id)
        {
            var data = _repo.Edit(Id);

            return Ok(data);
        }

    }
}
