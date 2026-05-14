using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.Lobby
{
    public sealed class LobbyExpiredException : BadRequestException
    {
        public LobbyExpiredException() : base("This lobby has expired. You can no longer send or view messages.")
        {
            
        }
    }
}
