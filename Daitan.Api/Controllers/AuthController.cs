using Daitan.Api.Controllers.Base;
using Daitan.Business.Helpers;
using Daitan.Data.Dto.Auth;
using Daitan.Data.Dto.Auths;
using Daitan.Data.Enums;
using Daitan.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace Daitan.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IConfiguration _configuration;
        private readonly EmailUtility _emailUtility;

        public AuthController(UserManager<ApplicationUser> userManager, 
            IConfiguration configuration,
            EmailUtility emailUtility,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailUtility = emailUtility;
            _roleManager = roleManager;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest("Account already exists");

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
            };

            var isSuccess = await _userManager.CreateAsync(user, model.Password);

            var isRoleAdded = await _userManager.AddToRoleAsync(user, UserRoleEnums.USER.ToString());

            if (!isRoleAdded.Succeeded)
            {
                return BadRequest("Invalid role");
            }

            if (isSuccess.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" });

                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var confirmationLink = string.Format("{0}/api/auth/confirmemail?userid={1}&token={2}", BaseUrl, user.Id, token);
                //var body = $"<p>Click the link below to continue:</p> <a href='{confirmationLink}'>confirm here</a>";
                //_emailUtility.SendEmailAsync("syed.rizwan@ambsan.com","Daitan account verification",  body);

                //return Ok(new { Message = "User registered successfully, plz verify your email" });
            }

            return BadRequest(isSuccess.Errors);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NotFound("No account found");
            }

            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //{
            //    var verifyToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //    var confirmationLink = 
            //        string.Format("{0}/auth/confirmemail?userid={1}&token={2}",
            //         BaseUrl, user.Id, verifyToken);
                
            //    var body = $"<p>Click the link below to continue:</p> <a href='{confirmationLink}'>confirm here</a>";
                
            //    _emailUtility.SendEmailAsync("syed.rizwan@ambsan.com", "Daitan account verification", body);
                

            //    return Unauthorized(new { Message = "Email not verified, email sent plz verify" });
            //}

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            var token = await GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userid, [FromQuery] string token)
        {
            var user = await _userManager.FindByIdAsync(userid);
            
            if (user == null)
                return NotFound("User not found");

            var strToken = HttpContext.Request.QueryString.Value.Split("token=")[1];

            var result = await _userManager.ConfirmEmailAsync(user, strToken);

            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully");
            }
            else
                return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var lstRole = await _userManager.GetRolesAsync(user);
            
            string roleId = string.Empty;
            string roleName = string.Empty;
            
            if (lstRole.Count() > 0)
            {
                roleName = lstRole.FirstOrDefault();
                var objRole = await _roleManager.FindByNameAsync(roleName);
                roleId = objRole.Id;
            }
            
            var claims = new[]
            {
            new Claim("roleid", roleId),
            new Claim("rolename", roleName),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
