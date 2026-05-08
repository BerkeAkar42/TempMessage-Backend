using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.MessageLobby
{
    public sealed class LobbyNotFoundException : NotFoundException
    {   //404
        public LobbyNotFoundException(Guid id) : base($"The lobby with id: {id} cloud not found.")
        {
            
        }
    }
}
