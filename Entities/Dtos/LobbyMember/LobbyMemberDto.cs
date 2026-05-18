using Entities.Dtos.Lobby;
using Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.LobbyMember
{
    public record LobbyMemberDto
    {
        public Guid LobbyMemberId { get; set; }
        public DateTime JoinedDate { get; init; }
        public bool IsAdmin { get; set; }

        public UserDto? User { get; set; }
        public LobbyDto? Lobby { get; set; }
    }
}
