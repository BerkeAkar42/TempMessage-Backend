using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.UserExceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {   // 404
        public UserNotFoundException(Guid id) : base($"The user with id: {id} cloud not found.")
        {
            
        }
    }
}
