using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataObjectModels.LobbyUsers
{
    public abstract record LobbyUsersDtoForManipulation
    {
        [Required(ErrorMessage = "MessageLobbyId is a required field.")]
        public Guid MessageLobbyId { get; set; }

        [Required(ErrorMessage = "UserId is a required field.")]
        public Guid UserId { get; set; }
    }
}
