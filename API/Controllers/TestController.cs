using Entities.DataObjectModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

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
        public IActionResult GetSecretData()
        {
            // Eğer buraya erişebiliyorsan Token geçerlidir.
            return Ok("Tebrikler Berke! Pasaportun (Token) geçerli, gizli bölgeye girdin.");
        }
    }
}
