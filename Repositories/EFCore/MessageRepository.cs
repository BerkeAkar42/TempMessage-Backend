using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.EFCore
{
    public class MessageRepository : RepositoryBase<Message> , IMessageRepository
    {
        public MessageRepository(RepositoriesContext context) : base(context)
        {
            
        }

        public void CreateOneMessage(Message message) => Create(message);
        

        public void DeleteOneMessage(Message message) => Delete(message);


        public async Task<IEnumerable<Message>> GetAllMessageAsync(bool trackChanges) => await FindAll(trackChanges).ToListAsync();


        public async Task<Message> GetOneMessageByIdAsync(Guid id, bool trackChanges) => await FindByCondition(m => m.MessageId == id, trackChanges).FirstOrDefaultAsync();
        

        public void UpdateOneMessage(Message message) => Update(message);
        
    }

}
