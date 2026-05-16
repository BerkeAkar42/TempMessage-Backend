using Entities.Dtos.User;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Lobby
{
    public record LobbyAuthResponseDto
    {
        public LobbyDto Lobby { get; set; }
        public UserAuthDto User { get; set; }
    }
}
