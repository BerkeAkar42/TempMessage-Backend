using Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(bool trackChanges);
        Task<UserDto> GetOneUserByIdAsync(Guid id, bool trackChanges);
        Task<UserAuthDto> CreateOneUserAsync(UserDtoForInsertion user); //Kullanıcı oluştuktan sonra kullanıcı bilgilerini tarayıcıya kayıt etmek üzere geri döndük. (id, NickName, AccessKey)
        Task UpdateOneUserAsync(Guid id, UserDtoForUpdate user, bool trackChanges); //--> Kullnıcıyı bulurken kullanacağımız GetOneUserById için trackChanges'i alıyoruz.
        Task DeleteOneUserAsync(Guid id, bool trackChanges);
        //Task<string> ExtendSessionAsync(Guid userId, int extraMinutes); //Kullanıcı başka bir lobiye katıldığında token süresini yenileyen logic olacak. Bunu Lobby manager'ında Create metotunun içinde çağırıp token işlemleri için buraya yönlendirebiliriz.
    }
}
