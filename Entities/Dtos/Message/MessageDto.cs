using Entities.Dtos.Lobby;
using Entities.Dtos.User;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Message
{   //Get isteklerinde kullanılacak Dto
    // API'den veri dönerken validasyona gerek yok. Sadece istemciye (Frontend) hangi verileri göstermek isteniyorsa onları koyarız.
    public record MessageDto
    {
        public Guid MessageId { get; init; }
        public string Content { get; set; }
        public DateTime SendDate { get; init; }
        public bool IsEdited { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public MessageType Type { get; set; }
        public Guid? ParentMessageId { get; set; }

        public UserDto? User { get; set; }
        public LobbyDto? Lobby { get; set; }
    }
}
