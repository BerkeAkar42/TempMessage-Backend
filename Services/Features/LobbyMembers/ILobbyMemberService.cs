using Entities.Dtos.LobbyMember;
using Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.LobbyMembers
{
    public interface ILobbyMemberService
    {
        /// <inheritdoc />
        Task<IEnumerable<LobbyMemberDto>> GetActiveMembershipsByUserIdAsync(Guid userId);

        /// <inheritdoc />
        Task<IEnumerable<UserDto>> GetLobbyParticipantsByLobbyIdAsync(Guid lobbyId);
    }
}
