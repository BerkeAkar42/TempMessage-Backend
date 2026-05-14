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
{
    public interface IServiceManager
    {
        ILobbyMemberService LobbyMemberService { get; }
        ILobbyService LobbyService { get; }
        IMessageService MessageService { get; }
        IUserService UserService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
