using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class MessageLobby
    {
        public Guid MessageLobbyId { get; init; }
        public string Name { get; set; }
        public DateTime CreateDate { get; init; }
        public int ValidityPeriod { get; set; }
        public bool IsActive { get; set; }

        public MessageLobby()
        {
            MessageLobbyId = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
    }
}
