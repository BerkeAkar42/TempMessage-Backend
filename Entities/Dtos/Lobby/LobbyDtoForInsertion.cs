using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Lobby
{   //Create işlemlerinde kullanılacak olan DTO
    public record LobbyDtoForInsertion : LobbyDtoForManipulation
    {
        //Yeni bir lobi oluştururken ID'ye ihtiyacımız yok (backend tarafından çağırılırken oluşturulacak). Bu yüzden sadece manipülasyon sınıfından miras alması yeterlidir.
    }
}
