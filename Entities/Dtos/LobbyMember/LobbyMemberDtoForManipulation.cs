using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.LobbyMember
{
    public abstract record LobbyMemberDtoForManipulation
    {
        [Required(ErrorMessage = "LobbyId is a required field.")]
        public Guid LobbyId { get; set; }

        [Required(ErrorMessage = "UserId is a required field.")]
        public Guid UserId { get; set; }
    }
}
