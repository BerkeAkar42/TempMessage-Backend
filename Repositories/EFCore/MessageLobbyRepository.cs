using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.EFCore
{
    public class MessageLobbyRepository : RepositoryBase<MessageLobby> , IMessageLobbyRepository
    {
        public MessageLobbyRepository(RepositoriesContext context) : base(context)
        {
            
        }

        public void CreateOneMessageLobby(MessageLobby messageLobby) => Create(messageLobby);
        

        public void DeleteOneMessageLobby(MessageLobby messageLobby) => Delete(messageLobby);
        

        public async Task<IEnumerable<MessageLobby>> GetAllMessageLobbiesAsync(bool trackChanges)
        {
            var messageLobbies = await FindAll(trackChanges).ToListAsync();
            return messageLobbies;
        }

        public async Task<MessageLobby> GetOneMessageLobbyByIdAsync(Guid id, bool trackChanges) => await FindByCondition(ml => ml.MessageLobbyId == id, trackChanges).FirstOrDefaultAsync();

        public void UpdateOneMessageLobby(MessageLobby messageLobby) => Update(messageLobby);
        
    }

}
