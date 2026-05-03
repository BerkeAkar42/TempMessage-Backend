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

        //FK tanımalamaları
        public Guid UserId { get; set; }
        public Guid MessageLobbyId { get; set; }

        public User User { get; set; }
        public MessageLobby MessageLobby { get; set; }

        public Message()
        {
            MessageId = Guid.NewGuid();
            SendDate = DateTime.Now;
        }
    }
}
