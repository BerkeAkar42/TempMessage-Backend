using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Authentication
{
    public interface IAuthenticationService
    {
        //User bilgileri gelecek.
        //Oda bilgileri gelmezse default olarak appsetting.json'daki "Expires" değeri baz alınsın.
        
        /// <summary>
        /// Token üretir
        /// </summary>
        /// <param name="user"></param>
        /// <param name="expireMinutes"></param>
        /// <returns></returns>
        string GenerateToken(User user, int? expireMinutes = null); //Token üretir

        /// <summary>
        /// Hesabı zaten olan veya ilk defa sisteme giriş yapmaya çalışan kullanıcıları ayırt eder. Kullanıcının token'ı yoksa geriye null döner
        /// </summary>
        /// <returns></returns>
        Guid? GetUserIdFromCurrentContext(); //Token içerisindeki id'yi çözer

        /// <summary>
        /// Kullanıcının token'ını kontrol eder. Doğrulanamazsa hata fırlatır.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        Guid GetUserId();
    }
}
