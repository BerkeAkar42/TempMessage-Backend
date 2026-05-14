using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{   //Tek tek her yerde DI yapmak yerine bunları tek bir yere yazıp her yerde erişebileceğim bir yapıya çevirdim.
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ILobbyUsersService> _lobbyUsersService;
        private readonly Lazy<ILobbyService> _LobbyService;
        private readonly Lazy<IMessageService> _messageService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration, ILoggerService logger)
        {
            _lobbyUsersService = new Lazy<ILobbyUsersService>(() => new LobbyUsersManager(repositoryManager, mapper, logger));
            _LobbyService = new Lazy<ILobbyService>(() => new LobbyManager(repositoryManager, mapper, logger));
            _messageService = new Lazy<IMessageService>(() => new MessageManager(repositoryManager, mapper, logger));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(configuration));
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager, mapper, AuthenticationService, logger));
        }

        public ILobbyUsersService LobbyUsersService => _lobbyUsersService.Value;
        public ILobbyService LobbyService => _LobbyService.Value;
        public IMessageService MessageService => _messageService.Value;
        public IUserService UserService => _userService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
