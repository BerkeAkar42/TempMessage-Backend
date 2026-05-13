using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.MessageLobby
{
    //Validation işlemlerinin tanımlandığı DTO
    //Bu sınıf new edilemez. Sadece validasyon kurallarını (zorunluluk, uzunluk vb.) tek bir merkezde toplar.
    public abstract record MessageLobbyDtoForManipulation
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [MinLength(3, ErrorMessage = "Name must consist of at least 3 characters")]
        [MaxLength(100, ErrorMessage = "Name must consist of at maximum 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "ValidityPeriod is a required field")]
        //[Range(1, 168, ErrorMessage = "Lobi süresi 1 ile 168 saat (1 hafta) arasında olmalıdır.")] Belirlenmedi!
        public int ValidityPeriod { get; set; }
    }
}
