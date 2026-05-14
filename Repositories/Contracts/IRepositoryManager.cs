using Repositories.EFCore.Lobbies;
using Repositories.EFCore.LobbyMembers;
using Repositories.EFCore.Messages;
using Repositories.EFCore.Users;
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
        ILobbyMemberRepository LobbyMember { get; }
        Task SaveAsync();
    }
}
