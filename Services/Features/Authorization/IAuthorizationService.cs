using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Authorization
{
    /// <summary>
    /// Authorization işlemleri buradan yapılır.
    /// </summary>
    public interface IAuthorizationService
    {
        #region Lobby Doğrulama Metotları
        /// <summary>
        /// "Kullanıcı bu lobby'nin üyesi mi?" kontrol eder
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lobbyId"></param>
        /// <returns></returns>
        Task CheckLobbyAccessAsync(Guid userId, Guid lobbyId);


        /// <summary>
        /// "Kullanıcı lobby owner mı?" kontrol eder
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lobbyId"></param>
        /// <returns></returns>
        Task CheckLobbyAdminshipAsync(Guid userId, Guid lobbyId);
        #endregion

        #region Message Doğrulama Metotları
        /// <summary>
        /// "Kullanıcı mesajın sahibi mi?" kontrol eder
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task CheckMessageOwnershipAsync(Guid userId, Guid messageId);
        #endregion
    }
}
