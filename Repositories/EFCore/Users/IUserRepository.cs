using Entities.Models;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Users
{
    public interface IUserRepository : IRepositoriesBase<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges);
        Task<User> GetOneUserByIdAsync(Guid id, bool trackChanges);
        void CreateOneUser(User user);
        void UpdateOneUser(User user);
        void DeleteOneUser(User user);
    }
}
