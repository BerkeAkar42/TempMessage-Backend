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
        Task<bool> IsUserMemberOfLobbyAsync(Guid userId, Guid lobbyId, bool trackChanges);
        Task<IEnumerable<LobbyMember>> GetActiveMembershipsByUserIdAsync(Guid userId, bool trackChanges);
        Task<IEnumerable<User>> GetLobbyParticipantsByLobbyIdAsync(Guid lobbyId, bool trackChanges);
    }

}
