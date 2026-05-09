using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.User
{
    public abstract record UserDtoForManipulation
    {
        [Required(ErrorMessage = "NickName is a required field.")]
        [MinLength(3, ErrorMessage = "NickName must consist of at least 3 characters")]
        [MaxLength(50, ErrorMessage = "NickName must consist of at maximum 50 characters")]
        public string? NickName { get; set; }
    }
}
