using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    //Tek tek her yerde DI yapmak yerine bunları tek bir yere yazıp her yerde erişebileceğim bir yapıya çevirdim.
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoriesContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IMessageRepository> _messageRepository;
        private readonly Lazy<ILobbyUsersRepository> _lobbyUsersRepository;
        private readonly Lazy<IMessageLobbyRepository> _messageLobbyRepository;

        public RepositoryManager(RepositoriesContext context)
        {
            _context = context;
            // Lazy: "Tembel Yükleme" (Lazy Loading) 
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _messageRepository = new Lazy<IMessageRepository>(() => new MessageRepository(_context));
            _lobbyUsersRepository = new Lazy<ILobbyUsersRepository>(()=> new LobbyUsersRepository(_context));
            _messageLobbyRepository = new Lazy<IMessageLobbyRepository>(()=> new MessageLobbyRepository(_context));
        }

        public IUserRepository User => _userRepository.Value;
        public IMessageRepository Message => _messageRepository.Value;
        public IMessageLobbyRepository MessageLobby => _messageLobbyRepository.Value;
        public ILobbyUsersRepository LobbyUsers => _lobbyUsersRepository.Value;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
        
    }
}
