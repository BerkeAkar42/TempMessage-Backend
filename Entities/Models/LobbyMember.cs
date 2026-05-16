using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class LobbyMember
    {
        public Guid LobbyMemberId { get; set; }
        public DateTime JoinedDate { get; init; }

        //Bir lobinin birden fazla admini olabilir...
        public bool IsAdmin { get; set; }

        //FK tanımalamaları
        public Guid LobbyId { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public Lobby Lobby { get; set; }

        public LobbyMember()
        {
            LobbyMemberId = Guid.NewGuid();
            JoinedDate = DateTime.UtcNow;
            IsAdmin = false;
        }
    }
}
