using Entities.Models;

namespace Repositories.Contracts
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