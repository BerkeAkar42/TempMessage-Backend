using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class LobbyUsers
    {
        public Guid LobbyUsersId { get; set; }
        public DateTime JoinedDate { get; init; }

        //FK tanımalamaları
        public Guid MessageLobbyId { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public MessageLobby MessageLobby { get; set; }

        public LobbyUsers()
        {
            LobbyUsersId = Guid.NewGuid();
            JoinedDate = DateTime.Now;
        }
    }
}
