using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User
    {
        public Guid UserId { get; init; } // Herkesin görebildiği Public ID
        public string NickName { get; set; }
        public string AccessKey { get; set; } // Şifre yerine geçecek gizli anahtar. Sadece sahibinde (LocalStorage) durur.
        public DateTime CreateDate { get; init; }
        public DateTime LastActiveDate { get; set; }

        public bool IsOnline => (DateTime.Now - LastActiveDate).TotalMinutes < 5;

        public User()
        {
            UserId = Guid.NewGuid();
            AccessKey = Guid.NewGuid().ToString("N"); // Tahmin edilemez benzersiz anahtar
            CreateDate = DateTime.Now;
            LastActiveDate = DateTime.Now;
        }
    }
}
