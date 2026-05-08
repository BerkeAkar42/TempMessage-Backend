using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{   // 400 - Kötü İstek hataları için temel sınıf
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message)
        {

        }
    }
}
