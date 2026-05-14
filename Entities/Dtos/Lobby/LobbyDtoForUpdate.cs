using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Lobby
{   //Güncelleme için kullanılacak olan DTO
    public record LobbyDtoForUpdate : LobbyDtoForManipulation
    {
        [Required(ErrorMessage = "LobbyId is a required field.")]
        public Guid LobbyId { get; init; }
    }
}
