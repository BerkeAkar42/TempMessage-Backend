using Entities.Models;

namespace Repositories.Contracts
{
    public interface ILobbyUsersRepository : IRepositoriesBase<LobbyUsers>
    {
        Task<IEnumerable<LobbyUsers>> GetAllLobbyUsersAsync(bool trackChanges);
        Task<LobbyUsers> GetOneLobbyUserByIdAsync(Guid id, bool trackChanges);
        void CreateOneLobbyUser(LobbyUsers lobbyUser);
        void UpdateOneLobbyUser(LobbyUsers lobbyUser);
        void DeleteOneLobbyUser(LobbyUsers lobbyUser);
    }

}
