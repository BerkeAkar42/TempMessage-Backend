using Entities.Models;

namespace Repositories.Contracts
{
    public interface IMessageLobbyRepository : IRepositoriesBase<MessageLobby>
    {
        Task<IEnumerable<MessageLobby>> GetAllMessageLobbiesAsync(bool trackChanges);
        Task<MessageLobby> GetOneMessageLobbyByIdAsync(Guid id, bool trackChanges);
        void CreateOneMessageLobby(MessageLobby messageLobby);
        void UpdateOneMessageLobby(MessageLobby messageLobby);
        void DeleteOneMessageLobby(MessageLobby messageLobby);
    }
}