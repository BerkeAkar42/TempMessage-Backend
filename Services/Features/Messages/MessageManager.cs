using AutoMapper;
using Entities.Dtos.Message;
using Entities.Exceptions.Lobby;
using Entities.Exceptions.MessageExceptions;
using Entities.Exceptions.UserExceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Messages;
using Repositories.Context;
using Services.Common;
using Services.Features.Authorization;
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
        private readonly IAuthorizationService _authorizationService;

        public MessageManager(IRepositoryManager manager, IMapper mapper, ILoggerService logger, IAuthorizationService authorizationService)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
        }

        public async Task<MessageDto> CreateOneMessageAsync(Guid lobbyId, Guid? userIdFromToken, MessageDtoForInsertion messageDto)
        {
            //Authorization olacak

            var lobby = await _manager.Lobby.GetOneLobbyByIdAsync(lobbyId, false);
            if (lobby is null)
                throw new LobbyNotFoundException(lobbyId);

            var user = await _manager.User.GetOneUserByIdAsync(userIdFromToken.Value, false);
            if (user is null)
                throw new UserNotFoundException(lobbyId);

            var message = _mapper.Map<Message>(messageDto);

            message.UserId = userIdFromToken.Value;
            message.LobbyId = lobbyId;

            _manager.Message.CreateOneMessage(message);
            await _manager.SaveAsync();

            //Gerekli bilgileri doldurduk.
            message.User = user;
            message.Lobby = lobby;

            return _mapper.Map<MessageDto>(message);
        }

        public async Task<MessageDto> DeleteOneMessageAsync(Guid messageId, Guid? userIdFromToken, Guid lobbyId)
        {
            //Authorization olacak

            var user = await _manager.User.GetOneUserByIdAsync(userIdFromToken.Value, false);
            if (user is null)
                throw new UserNotFoundException(userIdFromToken.Value);

            var lobby = await _manager.Lobby.GetOneLobbyByIdAsync(lobbyId, false);
            if (lobby is null)
                throw new LobbyNotFoundException(lobbyId);

            var message = await _manager.Message.GetOneMessageByIdAsync(messageId, true);
            if (message is null || message.IsDeleted == true)
                throw new MessageNotFoundException(messageId);

            message.Content = "Message deleted";
            message.IsDeleted = true;
            message.UpdateDate = DateTime.UtcNow;

            message.User = user;

            _manager.Message.UpdateOneMessage(message);
            await _manager.SaveAsync();

            return _mapper.Map<MessageDto>(message);
        }

        public async Task<(IEnumerable<MessageDto> messages, MetaData metaData)> GetMessagesByLobbyIdAsync(Guid lobbyId, MessageParameters messageParameters)
        {
            //Message ve meta data bilgilerini çek
            var messagesWithMetaData = await _manager.Message.GetMessagesByLobbyIdAsync(lobbyId, messageParameters, false);

            //Mesajları ayır
            var messageDto = _mapper.Map<IEnumerable<MessageDto>>(messagesWithMetaData);

            //Mesaj ve meta data bilgilerini ayrı olarak döndür
            return (messageDto, messagesWithMetaData.MetaData);
        }

        public async Task UpdateOneMessageAsync(Guid lobbyId, Guid? userIdFromToken, MessageDtoForUpdate messageDto)
        {
            //Authorization olacak

            var lobby = await _manager.Lobby.GetOneLobbyByIdAsync(lobbyId, false);
            if (lobby is null)
                throw new LobbyNotFoundException(lobbyId);

            var message = await _manager.Message.GetOneMessageByIdAsync(messageDto.MessageId, true);
            if (message is null || message.IsDeleted == true)
                throw new MessageNotFoundException(messageDto.MessageId);

            message.IsEdited = true;
            message.UpdateDate = DateTime.UtcNow;

            _mapper.Map(messageDto, message);
            _manager.Message.UpdateOneMessage(message);
            await _manager.SaveAsync();
        }
    }
}
