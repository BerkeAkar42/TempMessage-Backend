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
        public Guid UserId { get; init; }
        public string NickName { get; set; }
        public string AccessKey { get; set; }
        public DateTime CreateDate { get; init; }
        public DateTime LastActiveDate { get; set; }
    }
}
