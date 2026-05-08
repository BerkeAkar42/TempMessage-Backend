using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.UserExceptions
{   //401 - Yetkisiz Erişim
    public sealed class InvalidAccessKeyException : UnauthorizedAccessException
    {
        public InvalidAccessKeyException() : base("Invalid or incorrect access key.")
        {
            
        }
    }
}
