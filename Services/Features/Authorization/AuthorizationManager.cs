using Entities.Exceptions.Authorization;
using Entities.Exceptions.Lobby;
using Entities.Exceptions.LobbyMember;
using Entities.Exceptions.MessageExceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;

namespace Services.Features.Authorization
{
    public class AuthorizationManager : IAuthorizationService
    {
        private readonly IRepositoryManager _manager;

        public AuthorizationManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        #region Lobby Doğrulama Metotları
        /// <inheritdoc />
        public async Task CheckLobbyAccessAsync(Guid userId, Guid lobbyId)
        {
            var lobby = await _manager.Lobby.GetOneLobbyByIdAsync(lobbyId, false);
            if (lobby is null)
                throw new LobbyNotFoundException(lobbyId);

            var isMember = await _manager.LobbyMember
                .FindByCondition(lm => lm.UserId == userId && lm.LobbyId == lobbyId, false).AnyAsync();

            if (!isMember)
                throw new ForbiddenException("Unauthorized access request");
        }

        /// <inheritdoc />
        public async Task CheckLobbyAdminshipAsync(Guid userId, Guid lobbyId)
        {
            var lobby = await _manager.Lobby.GetOneLobbyByIdAsync(lobbyId, false);
            if (lobby is null)
                throw new LobbyNotFoundException(lobbyId);

            var lobbyMember = await _manager.LobbyMember.GetOneLobbyMemberByIdAsync(userId, lobbyId, false);
            if(lobbyMember is null || !lobbyMember.IsAdmin)
                throw new ForbiddenException("Unauthorized access request");
        }
        #endregion


        #region Messaj Doğrulama Metotları
        /// <inheritdoc />
        public async Task CheckMessageOwnershipAsync(Guid userId, Guid messageId)
        {
            var message = await _manager.Message.GetOneMessageByIdAsync(messageId, false);
            if (message is null)
                throw new MessageNotFoundException(messageId);

            var lobbyMember = await _manager.LobbyMember.GetOneLobbyMemberByIdAsync(userId, message.LobbyId, false);

            if (lobbyMember is null)
                throw new LobbyMemberNotFoundException(userId, message.LobbyId);

            if (message.UserId != userId && (!lobbyMember.IsAdmin))
                throw new ForbiddenException("You do not have permission to edit this message.");
        }
        #endregion
    }
}
