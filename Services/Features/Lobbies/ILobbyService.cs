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
        Task<LobbyAuthResponseDto> CreateOneLobbyAsync(LobbyDtoForInsertion lobbyDto, UserDtoForInsertion userDto, Guid? userIdFromToken);
        Task UpdateOneLobbyAsync(LobbyDtoForUpdate lobbyDto, bool trackChanges);
        Task DeleteOneLobbyAsync(Guid id, bool trackChanges);
        Task<LobbyAuthResponseDto> JoinLobbyAsync(Guid lobbyId, UserDtoForInsertion userDto, Guid? userIdFromToken);
    }
}
