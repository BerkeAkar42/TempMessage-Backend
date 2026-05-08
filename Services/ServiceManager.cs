using AutoMapper;
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
        private readonly Lazy<IMessageLobbyService> _messageLobbyService;
        private readonly Lazy<IMessageService> _messageService;
        private readonly Lazy<IUserService> _userService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _lobbyUsersService = new Lazy<ILobbyUsersService>(() => new LobbyUsersManager(repositoryManager, mapper));
            _messageLobbyService = new Lazy<IMessageLobbyService>(() => new MessageLobbyManager(repositoryManager, mapper));
            _messageService = new Lazy<IMessageService>(() => new MessageManager(repositoryManager, mapper));
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager, mapper));
        }

        public ILobbyUsersService LobbyUsersService => _lobbyUsersService.Value;
        public IMessageLobbyService MessageLobbyService => _messageLobbyService.Value;
        public IMessageService MessageService => _messageService.Value;
        public IUserService UserService => _userService.Value;
    }
}
