using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IMessageRepository Message { get; }
        ILobbyRepository Lobby { get; }
        ILobbyUsersRepository LobbyUsers { get; }
        Task SaveAsync();
    }
}
