using Entities.Dtos.Lobby;
using Entities.Dtos.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {   //TEST CONTROLLER
        private readonly IServiceManager _service;

        public LobbyController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Yeni bir lobi oluşturur. 
        /// Eğer kullanıcı login değilse (Token yoksa) UserDto zorunludur.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateLobby([FromBody] CreateLobbyRequest request)
        {
            // 1. Token'dan kullanıcı ID'sini al (Eğer varsa)
            var userIdFromToken = _service.AuthenticationService.GetUserIdFromCurrentContext();

            // 2. Servisi çağır
            var result = await _service.LobbyService.CreateOneLobbyAsync(
                request.LobbyDto,
                request.UserDto,
                userIdFromToken);

            return Ok(result);
        }

        /// <summary>
        /// Mevcut bir lobiye katılır.
        /// </summary>
        [HttpPost("join/{id:guid}")]
        public async Task<IActionResult> JoinLobby([FromRoute(Name = "id")] Guid id, [FromBody] UserDtoForInsertion? userDto)
        {
            // 1. Token'dan kullanıcı ID'sini al
            var userIdFromToken = _service.AuthenticationService.GetUserIdFromCurrentContext();

            // 2. Servisi çağır
            var result = await _service.LobbyService.JoinLobbyAsync(
                id,
                userDto,
                userIdFromToken);

            return Ok(result);
        }
    }
}
