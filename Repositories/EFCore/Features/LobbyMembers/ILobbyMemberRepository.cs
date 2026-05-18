using Entities.Models;
using Repositories.Context;

namespace Repositories.EFCore.Features.LobbyMembers
{
    public interface ILobbyMemberRepository : IRepositoriesBase<LobbyMember>
    {
        Task<IEnumerable<LobbyMember>> GetAllLobbyMembersAsync(bool trackChanges);
        Task<LobbyMember> GetOneLobbyMemberByIdAsync(Guid id, bool trackChanges);
        void CreateOneLobbyMember(LobbyMember lobbyMember);
        void UpdateOneLobbyMember(LobbyMember lobbyMember);
        void DeleteOneLobbyMember(LobbyMember lobbyMember);

        /// <summary>
        /// User belirtilen lobinin üyesi mi? kontrolü yapar.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lobbyId"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        Task<bool> IsUserMemberOfLobbyAsync(Guid userId, Guid lobbyId, bool trackChanges);

        /// <summary>
        /// User'ın aktif tüm lobilerini getirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        Task<IEnumerable<LobbyMember>> GetActiveMembershipsByUserIdAsync(Guid userId, bool trackChanges);


        /// <summary>
        /// Bir lobbideki user entity'lerini listeler.
        /// </summary>
        /// <param name="lobbyId"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<IEnumerable<User>> GetLobbyParticipantsByLobbyIdAsync(Guid lobbyId, bool trackChanges);
    }

}
