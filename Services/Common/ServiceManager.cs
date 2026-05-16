using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Repositories.Context;
using Services.Features.Authentication;
using Services.Features.Lobbies;
using Services.Features.LobbyMembers;
using Services.Features.Messages;
using Services.Features.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{   //Tek tek her yerde DI yapmak yerine bunları tek bir yere yazıp her yerde erişebileceğim bir yapıya çevirdim.
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ILobbyMemberService> _lobbyMemberService;
        private readonly Lazy<ILobbyService> _LobbyService;
        private readonly Lazy<IMessageService> _messageService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration, ILoggerService logger, IHttpContextAccessor httpContextAccessor)
        {
            _lobbyMemberService = new Lazy<ILobbyMemberService>(() => new LobbyMemberManager(repositoryManager, mapper, logger));
            _LobbyService = new Lazy<ILobbyService>(() => new LobbyManager(repositoryManager, mapper, logger, AuthenticationService));
            _messageService = new Lazy<IMessageService>(() => new MessageManager(repositoryManager, mapper, logger));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(configuration, httpContextAccessor));
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager, mapper, AuthenticationService, logger));
        }

        public ILobbyMemberService LobbyMemberService => _lobbyMemberService.Value;
        public ILobbyService LobbyService => _LobbyService.Value;
        public IMessageService MessageService => _messageService.Value;
        public IUserService UserService => _userService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
