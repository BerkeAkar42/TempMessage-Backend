using Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Lobby
{   //Veri taşıyıcı olarak kullanıyoruz.
    public record CreateLobbyRequest
    {   //Body'den tek bir veri çekebiliriz. Ayrı ayrı isteklerde bulunamayız. O yüzden bu parametreleri tek bir arada tutuyoruz.
        public LobbyDtoForInsertion LobbyDto { get; set; }
        public UserDtoForInsertion UserDto { get; set; }
    }
}
