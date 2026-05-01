using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class LobiUsers
    {
        public Guid LobiUsersId { get; set; }
        public DateTime JoinedDate { get; init; }

        //FK tanımalamaları
        public Guid MessageLobiId { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public MessageLobi MessageLobi { get; set; }

        public LobiUsers()
        {
            LobiUsersId = Guid.NewGuid();
            JoinedDate = DateTime.Now;
        }
    }
}
