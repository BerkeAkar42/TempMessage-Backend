using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.Message
{   //Get isteklerinde kullanılacak Dto
    // API'den veri dönerken validasyona gerek yok. Sadece istemciye (Frontend) hangi verileri göstermek isteniyorsa onları koyarız.
    public record MessageDto
    {
        public Guid MessageId { get; init; }
        public string Content { get; set; }
        public DateTime SendDate { get; init; }
        public bool IsEdited { get; set; }
        //public bool UpdateDate { get; set; }

        //FK tanımalamaları
        public Guid UserId { get; set; }
        public string NickName { get; set; }
        public Guid MessageLobbyId { get; set; }
    }
}
