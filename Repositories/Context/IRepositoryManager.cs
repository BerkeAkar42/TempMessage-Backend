using Repositories.EFCore.Features.Lobbies;
using Repositories.EFCore.Features.LobbyMembers;
using Repositories.EFCore.Features.Messages;
using Repositories.EFCore.Features.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Context
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
