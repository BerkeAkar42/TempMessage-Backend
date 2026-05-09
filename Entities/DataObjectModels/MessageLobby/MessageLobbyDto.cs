using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.MessageLobby
{   //GET işlemleri için kullanolacak olan DTO
    // API'den veri dönerken validasyona gerek yok. Sadece istemciye (Frontend) hangi verileri göstermek isteniyorsa onları koyarız.
    public record MessageLobbyDto
    {
        public Guid MessageLobbyId { get; init; }
        public string? Name { get; set; }
        public DateTime CreateDate { get; init; }
        public int ValidityPeriod { get; set; }
        public bool IsActive { get; set; }
    }
}
