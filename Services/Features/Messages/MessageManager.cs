using AutoMapper;
using Entities.Dtos.Message;
using Entities.Exceptions.MessageExceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Messages;
using Repositories.Context;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Messages
{
    public class MessageManager : IMessageService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public MessageManager(IRepositoryManager manager, IMapper mapper, ILoggerService logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MessageDto> CreateOneMessageAsync(MessageDtoForInsertion messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);

            _manager.Message.CreateOneMessage(message);
            await _manager.SaveAsync();

            return _mapper.Map<MessageDto>(message);
        }

        public async Task<MessageDto> DeleteOneMessageAsync(Guid messageId)
        {
            var message = await _manager.Message.GetOneMessageByIdAsync(messageId, true);
            if (message is null || message.IsDeleted == true)
                throw new MessageNotFoundException(messageId);

            message.Content = "Message deleted";
            message.IsDeleted = true;
            message.UpdateDate = DateTime.UtcNow;

            _manager.Message.UpdateOneMessage(message);
            await _manager.SaveAsync();

            return _mapper.Map<MessageDto>(message);
        }

        public Task<(IEnumerable<MessageDto> messages, MetaData metaData)> GetMessagesByLobbyIdAsync(Guid lobbyId, MessageParameters messageParameters)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateOneMessageAsync(Guid messageId, MessageDtoForUpdate messageDto)
        {
            var message = await _manager.Message.GetOneMessageByIdAsync(messageId, true);
        }
    }
}
