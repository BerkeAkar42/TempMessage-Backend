using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.MessageLobby
{   //Güncelleme için kullanılacak olan DTO
    public record MessageLobbyDtoForUpdate : MessageLobbyDtoForManipulation
    {
        [Required(ErrorMessage = "MessageLobbyId is a required field.")]
        public Guid MessageLobbyId { get; init; }
    }
}
