using AutoMapper;
using Entities.Dtos.LobbyMember;
using Entities.Dtos.User;
using Entities.Exceptions.Lobby;
using Entities.Exceptions.UserExceptions;
using Repositories.Context;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.LobbyMembers
{
    public class LobbyMemberManager : ILobbyMemberService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public LobbyMemberManager(IRepositoryManager manager, IMapper mapper, ILoggerService logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<LobbyMemberDto>> GetActiveMembershipsByUserIdAsync(Guid userId)
        {
            var user = await _manager.User.GetOneUserByIdAsync(userId, false);

            if (user is null)
                throw new UserNotFoundException(userId);

            //User'ın aktif lobilerini çeker
            var userLobbies = await _manager.LobbyMember.GetActiveMembershipsByUserIdAsync(userId, false);

            return _mapper.Map<IEnumerable<LobbyMemberDto>>(userLobbies);
        }

        public async Task<IEnumerable<UserDto>> GetLobbyParticipantsByLobbyIdAsync(Guid lobbyId)
        {   //Lobby içerisindeki kullanıcıları listeler
            var lobby = await _manager.Lobby.GetOneLobbyByIdAsync(lobbyId, false);

            if(lobby is null)
                throw new LobbyNotFoundException(lobbyId);

            var users = _manager.LobbyMember.GetLobbyParticipantsByLobbyIdAsync(lobbyId, false);
            
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
