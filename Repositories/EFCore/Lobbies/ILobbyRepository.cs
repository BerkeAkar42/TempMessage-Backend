using Entities.Models;
using Repositories.Context;

namespace Repositories.EFCore.Lobbies
{
    public interface ILobbyRepository : IRepositoriesBase<Lobby>
    {
        Task<IEnumerable<Lobby>> GetAllLobbiesAsync(bool trackChanges);
        Task<Lobby> GetOneLobbyByIdAsync(Guid id, bool trackChanges);
        void CreateOneLobby(Lobby Lobby);
        void UpdateOneLobby(Lobby Lobby);
        void DeleteOneLobby(Lobby Lobby);
    }
}