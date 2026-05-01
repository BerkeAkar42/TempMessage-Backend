using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class MessageLobi
    {
        public Guid MessageLobiId { get; init; }
        public string Name { get; set; }
        public DateTime CreateDate { get; init; }
        public int ValidityPeriod { get; set; }
        public bool IsActive { get; set; }

        public MessageLobi()
        {
            MessageLobiId = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
    }
}
