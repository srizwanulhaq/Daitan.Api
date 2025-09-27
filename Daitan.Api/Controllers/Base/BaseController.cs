using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Daitan.Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string BaseUrl
        {
            get
            {
                return $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            }
        }
        protected string CurrentUserId
        {
            get
            {
                return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }

        protected ActionResult ApiResponse(object result, string message = "Success", int statusCode = 200)
        {
            var response = new 
            {
                Message = message,
                data = result,
                IsError = false,
                StatusCode = statusCode
            };

            return StatusCode(statusCode, response);
        }
    }
}
