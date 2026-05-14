using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore.LobbyMembers
{
    public interface ILobbyMemberRepository : IRepositoriesBase<LobbyMember>
    {
        Task<IEnumerable<LobbyMember>> GetAllLobbyMembersAsync(bool trackChanges);
        Task<LobbyMember> GetOneLobbyMemberByIdAsync(Guid id, bool trackChanges);
        void CreateOneLobbyMember(LobbyMember lobbyMember);
        void UpdateOneLobbyMember(LobbyMember lobbyMember);
        void DeleteOneLobbyMember(LobbyMember lobbyMember);
    }

}
