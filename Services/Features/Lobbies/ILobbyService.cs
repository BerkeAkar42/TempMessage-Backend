using Entities.Dtos.Lobby;
using Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Lobbies
{
    public interface ILobbyService
    {
        /// <summary>
        /// Yeni lobby açma metotu.
        /// </summary>
        /// <param name="lobbyDto">Lobby bilgilerini içerir</param>
        /// <param name="userDto">User bilgilerini içerir</param>
        /// <param name="userIdFromToken">User token'a sahipse id döner. Yeni oluşturulan odalara göre user yaşam süresi hesaplanır</param>
        /// <returns>LobbyAuthResponseDto (LobbyDto, UserDto)</returns>
        /// <exception cref="UserNotFoundException">Kullanıcı bulunamdı hatası</exception>
        Task<LobbyAuthResponseDto> CreateOneLobbyAsync(LobbyDtoForInsertion lobbyDto, UserDtoForInsertion userDto, Guid? userIdFromToken);

        
        Task UpdateOneLobbyAsync(LobbyDtoForUpdate lobbyDto);
        Task DeleteOneLobbyAsync(Guid id);


        /// <summary>
        /// Lobby'e katılma metotu
        /// </summary>
        /// <param name="lobbyId">Aktif olan lobinin id'si</param>
        /// <param name="userDto">User bilgisi yoksa, user kayıt için alınan bilgiler</param>
        /// <param name="userIdFromToken">User token'a sahipse id döner. Yeni oluşturulan odalara göre user yaşam süresi hesaplanır</param>
        /// <returns>LobbyAuthResponseDto (LobbyDto, UserDto)</returns>
        /// <exception cref="LobbyNotFoundException">Lobby bulunamadı hatası</exception>
        /// <exception cref="UserNotFoundException">User bulunaamadı hatası</exception>
        Task<LobbyAuthResponseDto> JoinLobbyAsync(Guid lobbyId, UserDtoForInsertion userDto, Guid? userIdFromToken);
    }
}
