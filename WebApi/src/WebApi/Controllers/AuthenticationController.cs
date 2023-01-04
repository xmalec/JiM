using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers
{

    public class AuthenticationController : ApiControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userService.LoginUser(model.Email, model.Password);
            if (user == null)
            {
                return BadRequest();
            }
            var identity = user.Adapt<UserIdentityModel>();
            var claims = new[] {
                        new Claim(ClaimTypes.Role, Roles.Admin),
                        new Claim("UserID", identity.Id)
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                        jwtOptions.Issuer,
                        jwtOptions.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);
            identity.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(identity);
        }

    }
}