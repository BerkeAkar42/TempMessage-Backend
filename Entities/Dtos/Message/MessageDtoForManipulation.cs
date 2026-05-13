using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Message
{
    public abstract record MessageDtoForManipulation
    {
        [Required(ErrorMessage = "Message content is a required field.")]
        public string? Content { get; set; }
    }
}
