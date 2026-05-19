using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Messages;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.EFCore.Extensions;
using System.Linq.Expressions;

namespace Repositories.EFCore.Features.Messages
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(RepositoriesContext context) : base(context)
        {

        }

        public void CreateOneMessage(Message message) => Create(message);


        public void DeleteOneMessage(Message message) => Delete(message);

        public async Task<PagedList<Message>> GetMessagesByLobbyIdAsync(Guid lobbyId, MessageParameters messageParameters, bool trackChanges)
        {
            //Chat akışı için SendDate'e göre sıralama yapıyoruz
             IQueryable<Message> messages = FindByCondition(m => m.LobbyId == lobbyId, trackChanges)
                .Include(m => m.User)
                .Include(m => m.Lobby); //sql sorgusu hazırla...

            //sql sorgusu devam ediyor...
            if (!string.IsNullOrWhiteSpace(messageParameters.SearchTerm)) //Eğer kullanıcı arama yaptıysa, sorguya bir where ekle... 
            {
                var searchTerm = messageParameters.SearchTerm.Trim().ToLower();
                messages = messages.Where(m => m.Content.ToLower().Contains(searchTerm));
            }

            //default olarak SendDate'e göre sırala.
            messages = messages.OrderBy(m => m.SendDate);

            //db'ye gidip sql sorgusu çalıştırıldı.
            return await messages.ToPagedListAsync(messageParameters.PageNumber, messageParameters.PageSize);
        }

        public async Task<Message> GetOneMessageByIdAsync(Guid id, bool trackChanges) => await FindByCondition(m => m.MessageId == id, trackChanges).FirstOrDefaultAsync();


        public void UpdateOneMessage(Message message) => Update(message);

    }

}
