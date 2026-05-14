using Entities.Models;
using Repositories.Context;

namespace Repositories.EFCore.Messages
{
    public interface IMessageRepository : IRepositoriesBase<Message>
    {
        Task<IEnumerable<Message>> GetAllMessageAsync(bool trackChanges);
        Task<Message> GetOneMessageByIdAsync(Guid id, bool trackChanges);
        void CreateOneMessage(Message message);
        void UpdateOneMessage(Message message);
        void DeleteOneMessage(Message message);
    }
}
