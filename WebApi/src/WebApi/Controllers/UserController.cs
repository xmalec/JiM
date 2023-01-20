using BL.Models.User;
using BL.Services.User;
using Extensions.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            return userService.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserDto Get(int id)
        {
            return userService.GetById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserCreateModel userCreateModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await userService.Add(userCreateModel.Map<UserDto>());
            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserDto userDto)
        {
            userDto.Id = id;
            userService.Update(userDto);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userService.Delete(id);
        }
    }
}
