using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyMemberController : ControllerBase
    {
        private readonly IServiceManager _service;

        public LobbyMemberController(IServiceManager service)
        {
            _service = service;
        }


        /// <summary>
        /// Belirli bir kullanıcının tüm AKTİF lobilerini listeler (Sol Panel).
        /// </summary>
        [HttpGet("user")]
        public async Task<IActionResult> GetActiveMembershipsByUserId()
        {
            var userId = _service.AuthenticationService.GetUserId();

            var result = await _service.LobbyMemberService.GetActiveMembershipsByUserIdAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Belirli bir lobiye dahil olan tüm kullanıcıları listeler (Sağ Panel).
        /// </summary>
        [HttpGet("lobby/{lobbyId:guid}/participants")]
        public async Task<IActionResult> GetLobbyParticipantsByLobbyId([FromRoute] Guid lobbyId)
        {
            var userId = _service.AuthenticationService.GetUserId();

            var result = await _service.LobbyMemberService.GetLobbyParticipantsByLobbyIdAsync(lobbyId, userId);
            return Ok(result);
        }
    }
}
