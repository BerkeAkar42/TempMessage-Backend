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
        private readonly Lazy<ILobbyMemberRepository> _lobbyMembersRepository;
        private readonly Lazy<ILobbyRepository> _LobbyRepository;

        public RepositoryManager(RepositoriesContext context)
        {
            _context = context;
            // Lazy: "Tembel Yükleme" (Lazy Loading) 
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _messageRepository = new Lazy<IMessageRepository>(() => new MessageRepository(_context));
            _lobbyMembersRepository = new Lazy<ILobbyMemberRepository>(()=> new LobbyMemberRepository(_context));
            _LobbyRepository = new Lazy<ILobbyRepository>(()=> new LobbyRepository(_context));
        }

        public IUserRepository User => _userRepository.Value;
        public IMessageRepository Message => _messageRepository.Value;
        public ILobbyRepository Lobby => _LobbyRepository.Value;
        public ILobbyMemberRepository LobbyMember => _lobbyMembersRepository.Value;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
        
    }
}
