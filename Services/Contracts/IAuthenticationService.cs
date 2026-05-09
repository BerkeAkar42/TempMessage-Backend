using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        //User bilgileri gelecek.
        //Oda bilgileri gelmezse default olarak appsetting.json'daki "Expires" değeri baz alınsın.
        string GenerateToken(User user, int? expireMinutes = null);
    }
}
