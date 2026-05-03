using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User
    {
        public Guid UserId { get; init; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; init; }
        public bool IsActive { get; set; }
        public DateTime LastActiveDate { get; set; }

        // Navigation Properties (İlişkiler)
        //public ICollection<LobiUsers> LobiUsers { get; set; }
        //public ICollection<Message> Messages { get; set; }
        //Bu gelen veriler için ayrıca bir servis yazılabilir


        public User()
        {
            UserId = Guid.NewGuid();
            CreateDate = DateTime.Now;

            //LobiUsers = getLobiUserData(); --> çağırıldığında fonksiyon tetiklenerek getirilsin diyebiliriz.
            //Messages = getMessagesData(); --> çağırıldığında fonksiyon tetiklenerek getirilsin diyebiliriz.
        }
    }
}
