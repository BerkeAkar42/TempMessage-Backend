using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.LobbyUsers
{
    public record LobbyUsersDto
    {
        public Guid LobbyUsersId { get; set; }
        public DateTime JoinedDate { get; init; }
        public Guid UserId { get; set; }
        public string? NickName { get; set; }
        public Guid LobbyId { get; set; }
    }
}
