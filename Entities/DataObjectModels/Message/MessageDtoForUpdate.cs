using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.Message
{   //Güncelleme için kullanılacak olan DTO
    public record MessageDtoForUpdate : MessageDtoForManipulation
    {
        [Required(ErrorMessage = "MessageId is a required field.")]
        public Guid MessageId { get; init; }
    }
}
