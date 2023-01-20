using AutoMapper;
using BL.Services.Identity;
using BL.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{

    public class AuthenticationController : ApiControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserService userService;
        private readonly IIdentityService identityService;

        public AuthenticationController(ILogger<AuthenticationController> logger,
            IUserService userService,
            IIdentityService identityService)
        {
            _logger = logger;
            this.userService = userService;
            this.identityService = identityService;
        }

        [HttpPost]
        [AllowAnonymous]
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
            var identity = identityService.GetIdentity(user);
            return Ok(identity);
        }

    }
}