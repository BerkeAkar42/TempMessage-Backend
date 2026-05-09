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

        /*
         UserId: "Ben kimim?"
         NickName: "Adım ne?"
         AccessKey: "Gizli anahtarım ne?"
         Token: "İçeriye girmek için iznim var mı?"
         */
    }
}
