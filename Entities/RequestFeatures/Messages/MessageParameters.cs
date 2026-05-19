using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures.Messages
{
    public class MessageParameters : RequestParameters
    {
        // Mesajlarda arama yapmak istersek (Örn: "selam" geçen mesajlar)
        public string? SearchTerm { get; set; }

        // Sıralama varsayılan olarak en yeni mesaj en altta olacak şekilde olsun
        public MessageParameters()
        {
            OrderBy = "SendDate";
        }
    }
}
