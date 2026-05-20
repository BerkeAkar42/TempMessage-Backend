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
        /// <summary>
        /// Kullanıcının aktif lobilerini getirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<LobbyMemberDto>> GetActiveMembershipsByUserIdAsync(Guid userId);

        /// <summary>
        /// Bir lobinin tüm kullanıcılarını listeler.
        /// </summary>
        /// <param name="lobbyId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserDto>> GetLobbyParticipantsByLobbyIdAsync(Guid lobbyId, Guid userId);
    }
}
