using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.User
{
    public record UserAuthDto
    {
        public Guid UserId { get; init; }
        public string NickName { get; set; }
        public string AccessKey { get; set; }
    }
}
