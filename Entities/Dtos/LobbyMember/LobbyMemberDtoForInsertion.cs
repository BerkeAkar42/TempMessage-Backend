using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.LobbyMember
{
    public record LobbyMemberDtoForInsertion : LobbyMemberDtoForManipulation
    {
        //LobbyMember oluşturulurken manipülasyon propları yeterlidir.
    }
}
