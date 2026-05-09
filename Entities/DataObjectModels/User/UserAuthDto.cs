using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.User
{
    public record UserAuthDto
    {
        /// <example>dacb87b8-e262-4918-825d-5e9c4b204632</example>
        public Guid UserId { get; init; }
        /// <example>BerkeAkar</example>
        public string? NickName { get; set; }
        /// <example>e02aa315a43641b992f45c3abb19d2ae</example>
        public string? AccessKey { get; set; }
        /// <example></example>
        public string? Token { get; set; }

        /*
         UserId: "Ben kimim?"
         NickName: "Adım ne?"
         AccessKey: "Gizli anahtarım ne?"
         Token: "İçeriye girmek için iznim var mı?"
         */
    }
}
