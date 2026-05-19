using Entities.Dtos.Message;
using Entities.RequestFeatures.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common;
using System.Text.Json;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public MessagesController(IServiceManager service)
        {
            _service = service;
        }

        // 1. Lobi Mesajlarını Getir (Sayfalamalı ve Filtreli)
        // GET: api/messages/lobby/{lobbyId}
        [HttpGet("lobby/{lobbyId:guid}")]
        public async Task<IActionResult> GetMessagesByLobbyId([FromRoute] Guid lobbyId, [FromQuery] MessageParameters messageParameters)
        {
            var pagedResult = await _service.MessageService.GetMessagesByLobbyIdAsync(lobbyId, messageParameters);

            // Zafer Hoca'nın meşhur Header dokunuşu
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.messages);
        }

        // 2. Mesaj Gönder
        // POST: api/messages
        [HttpPost("lobby/{lobbyId:guid}")]
        public async Task<IActionResult> CreateMessage([FromRoute] Guid lobbyId, [FromBody] MessageDtoForInsertion messageDto)
        {
            var userIdFromToken = _service.AuthenticationService.GetUserIdFromCurrentContext();

            var message = await _service.MessageService.CreateOneMessageAsync(lobbyId, userIdFromToken, messageDto);

            // Gerçek bir REST API'de CreatedAtRoute veya CreatedAtAction dönmek daha şıktır
            return StatusCode(201, message);
        }

        // 3. Mesaj Düzenle
        // PUT: api/messages/{id}
        [HttpPut("lobby/{lobbyId:guid}")]
        public async Task<IActionResult> UpdateMessage([FromRoute] Guid lobbyId, [FromBody] MessageDtoForUpdate messageDto)
        {
            var userIdFromToken = _service.AuthenticationService.GetUserIdFromCurrentContext();

            await _service.MessageService.UpdateOneMessageAsync(lobbyId, userIdFromToken, messageDto);
            return NoContent(); // 204 döneriz
        }

        // 4. Mesaj Sil (Soft Delete)
        // DELETE: api/messages/{id}
        [HttpDelete("lobby/{lobbyId:guid}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] Guid lobbyId, [FromBody] Guid messageId)
        {
            var userIdFromToken = _service.AuthenticationService.GetUserIdFromCurrentContext();

            var result = await _service.MessageService.DeleteOneMessageAsync(messageId, userIdFromToken, lobbyId);
            return Ok(result); // Silindi bilgisini (IsDeleted: true) dönmek için 200 Ok
        }
    }
}
