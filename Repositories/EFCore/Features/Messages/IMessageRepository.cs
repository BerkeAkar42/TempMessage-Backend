using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Messages;
using Repositories.Context;

namespace Repositories.EFCore.Features.Messages
{
    public interface IMessageRepository : IRepositoriesBase<Message>
    {
        Task<PagedList<Message>> GetMessagesByLobbyIdAsync(Guid lobbyId, MessageParameters messageParameters, bool trackChanges);
        Task<Message> GetOneMessageByIdAsync(Guid id, bool trackChanges);
        void CreateOneMessage(Message message);
        void UpdateOneMessage(Message message);
        void DeleteOneMessage(Message message);
    }
}
