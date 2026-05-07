using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public UserRepository(RepositoriesContext context) : base(context)
        {
        }

        public void CreateOneUser(User user) => Create(user);
        
        public void DeleteOneUser(User user) => Delete(user);

        public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges)
        {
            //var users = await FindAll(trackChanges).Where(u => u.IsActive == true).ToListAsync(); //Örnek: Aktif olan hesapları dön
            var users = await FindAll(trackChanges).ToListAsync();
            return users;
        }

        public async Task<User> GetOneUserByIdAsync(Guid id, bool trackChanges)
        {
            var user = await FindByCondition(u => u.UserId == id, trackChanges).SingleOrDefaultAsync();
            return user;
        }

        public void UpdateOneUser(User user) => Update(user);
        
    }
}
