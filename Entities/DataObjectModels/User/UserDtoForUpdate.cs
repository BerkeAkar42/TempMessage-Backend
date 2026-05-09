using System.ComponentModel.DataAnnotations;

namespace Entities.DataObjectModels.User
{
    public record UserDtoForUpdate : UserDtoForManipulation
    {
        [Required(ErrorMessage = "UserId is a required field.")]
        public Guid UserId { get; init; }
    }
}
