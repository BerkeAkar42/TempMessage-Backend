using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum MessageType
    {
        User = 0,   // Normal kullanıcı mesajı
        System = 1, // "Katıldı", "Ayrıldı" gibi sistem uyarıları
        Alert = 2   // "Süre bitiyor!" gibi kritik uyarılar
    }

    public class Message
    {
        public Guid MessageId { get; init; }
        public string Content { get; set; }
        public DateTime SendDate { get; init; }
        public bool IsEdited { get; set; } //Update isteği geldiği sırada logic içerisinde true olacak bir proptur.
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; } //F.E. tarafında mesaj silindi yazdırabilmek için. (mesaj silme isteği geldiği anda mesaj içeriği null yapılıp bu değeri true yaparız. Mesaj böylelikle silinmiş olur.)
        public MessageType Type { get; set; } //Mesajın tipi
        public Guid? ParentMessageId { get; set; } //Bir mesaja cevap olarak yazılırsa o mesajın id değeri burada tutulacak.

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
            IsDeleted = false; //Daha silinmedi.
            Type = MessageType.User; //Kullanıcı mesajı olarak ayarlandı.
        }
    }
}
