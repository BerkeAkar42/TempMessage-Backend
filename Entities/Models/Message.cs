using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Message
    {
        public Guid MessageId { get; init; }
        public string Content { get; set; }
        public DateTime SendDate { get; init; }
        public bool IsEdited { get; set; } //Update isteği geldiği sırada logic içerisinde true olacak bir proptur.
        public DateTime UpdateDate { get; set; }

        //FK tanımalamaları
        public Guid UserId { get; set; }
        public Guid LobbyId { get; set; }

        public User User { get; set; }
        public Lobby Lobby { get; set; }

        public Message()
        {
            MessageId = Guid.NewGuid();
            SendDate = DateTime.UtcNow;
            IsEdited = false; //Daha düzenlenmedi.
        }
    }
}
