using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.LobbyUsers
{
    public record LobbyUsersDto
    {
        public Guid LobbyUsersId { get; set; }
        public DateTime JoinedDate { get; init; }
        public Guid UserId { get; set; }
        public string? NickName { get; set; }
        public Guid MessageLobbyId { get; set; }
    }
}
