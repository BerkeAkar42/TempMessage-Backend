using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.LobbyUsers
{   //404
    public sealed class LobbyUserNotFoundException : NotFoundException
    {
        public LobbyUserNotFoundException(Guid userId, Guid lobbyId) : base($"The user (ID: {userId}) could not be found in the lobby (ID: {lobbyId}).")
        {
            
        }
    }
}
