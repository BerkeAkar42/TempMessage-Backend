using Entities.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TestController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost] //1. TEST: Kullanıcı Oluştur ve Token Al (Public)
        public async Task<IActionResult> CreateUser([FromBody] UserDtoForInsertion userDto)
        {
            if (userDto is null)
                return BadRequest();

            var result = await _service.UserService.CreateOneUserAsync(userDto);

            return StatusCode(201, result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.UserService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOneUser([FromRoute(Name ="id")] Guid id)
        {
            if(id == Guid.Empty)
                return BadRequest();

            await _service.UserService.DeleteOneUserAsync(id);
            return NoContent();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOneUser(UserDtoForUpdate newUser)
        {
            if (newUser is null)
                return BadRequest();

            await _service.UserService.UpdateOneUserAsync(newUser.UserId, newUser);
            return NoContent();
        }
    }
}
