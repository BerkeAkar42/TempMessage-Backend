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


        /* Burası veritabanını yorabilecek bir yapıdaymış.
         * - IsActive Yapılandırması -
         * Message.cs -> Burada sendDate var.
         * 1- o an gelen mesajın içerisindeki gönderen kişinin id si berke123 ise elinde tut.
         * 2- bu kısımda işte o gönderilme tarihi kontrol edilip işte gönderim tarihinden 5 dk geçmediyse IsActive değerini true, geçtiyse false yaz.
         * 3- son mesajın içerisindeki sendDate tarihini id si berke123 olan kişinin lastActiveDate'ine yaz.
         */

        //Bu kod parçası gemini tarafından önerildi ve veritabanını yormayan bir yapıya sahip.
        // Bu alan veritabanında yer tutmaz, her çağırıldığında hesaplanır
        //public bool IsOnline => (DateTime.Now - LastActiveDate).TotalMinutes < 5;
        //Kullanıcı her sayfayı yenilediğinde bu istek de gider böylelikle aslında sorgu işiyle uğraşmamış oluruz.


        public User()
        {
            UserId = Guid.NewGuid();
            CreateDate = DateTime.Now;

            //LobiUsers = getLobiUserData(); --> çağırıldığında fonksiyon tetiklenerek getirilsin diyebiliriz.
            //Messages = getMessagesData(); --> çağırıldığında fonksiyon tetiklenerek getirilsin diyebiliriz.
        }
    }
}
