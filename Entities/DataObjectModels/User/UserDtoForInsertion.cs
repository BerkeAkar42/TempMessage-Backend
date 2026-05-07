using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.User
{
    public record UserDtoForInsertion : UserDtoForManipulation
    {
        // Yeni kullanıcı oluştururken dışarıdan sadece NickName almamız yeterli.
    }
}
