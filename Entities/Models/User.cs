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

        public bool IsOnline => (DateTime.UtcNow - LastActiveDate).TotalMinutes < 5;

        public User()
        {
            UserId = Guid.NewGuid(); // e02aa315-a436-41b9-92f4-5c3abb19d2ae
            AccessKey = Guid.NewGuid().ToString("N"); // e02aa315a43641b992f45c3abb19d2ae
            CreateDate = DateTime.UtcNow;
            LastActiveDate = DateTime.UtcNow;
        }
    }
}
