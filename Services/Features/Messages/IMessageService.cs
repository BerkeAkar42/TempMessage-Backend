using Entities.Dtos.Message;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Messages
{
    public interface IMessageService
    {
        /// <summary>
        /// İlgili lobideki tüm mesajları listeler (pagination)
        /// </summary>
        /// <param name="lobbyId"></param>
        /// <param name="messageParameters">Pagination</param>
        /// <returns>Messages and Meta Data</returns>
        Task<(IEnumerable<MessageDto> messages, MetaData metaData)> GetMessagesByLobbyIdAsync(Guid lobbyId, MessageParameters messageParameters);
        
        /// <summary>
        /// İlgili lobi içerisinde bir mesaj oluşturur
        /// </summary>
        /// <param name="messageDto"></param>
        /// <returns></returns>
        Task<MessageDto> CreateOneMessageAsync(Guid lobbyId, Guid? userIdFromToken, MessageDtoForInsertion messageDto);

        /// <summary>
        /// İlgili lobideki bir mesajı günceller
        /// </summary>
        /// <param name="messageDto"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        Task UpdateOneMessageAsync(Guid lobbyId, Guid? userIdFromToken, MessageDtoForUpdate messageDto);


        /// <summary>
        /// İlgili lobideki bir mesajı siler
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        Task<MessageDto> DeleteOneMessageAsync(Guid messageId, Guid? userIdFromToken, Guid lobbyId);
    }
}
