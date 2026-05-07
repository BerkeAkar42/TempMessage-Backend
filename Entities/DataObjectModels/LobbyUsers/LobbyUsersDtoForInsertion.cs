using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.LobbyUsers
{
    public record LobbyUsersDtoForInsertion : LobbyUsersDtoForManipulation
    {
        //LobbyUsers oluşturulurken manipülasyon propları yeterlidir.
    }
}
