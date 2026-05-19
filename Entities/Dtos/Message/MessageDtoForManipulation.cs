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
        [MinLength(1, ErrorMessage = "Context must consist of at least 1 characters")]
        [MaxLength(1000, ErrorMessage = "Context must consist of at maximum 1000 characters")]
        public string? Content { get; set; }
    }
}
