using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.User
{   //Get isteklerinde kullanılacak Dto
    // API'den veri dönerken validasyona gerek yok. Sadece istemciye (Frontend) hangi verileri göstermek isteniyorsa onları koyarız.
    public record UserDto
    {
        /// <example>dacb87b8-e262-4918-825d-5e9c4b204632</example>
        public Guid UserId { get; init; }
        /// <example>BerkeAkar</example>
        public string NickName { get; set; }
        /// <example>8.05.2026 23:25:28</example>
        public DateTime CreateDate { get; init; }
        /// <example>8.05.2026 23:25:28</example>
        public DateTime LastActiveDate { get; set; }
    }
}
