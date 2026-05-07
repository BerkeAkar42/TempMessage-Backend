using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.Message
{
    public record MessageDtoForInsertion : MessageDtoForManipulation
    {
        [Required(ErrorMessage = "UserId is a required field.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "MessageLobbyId is a required field.")]
        public Guid MessageLobbyId { get; set; }
    }
}
