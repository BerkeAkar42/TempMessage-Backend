using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.MessageExceptions
{
    public sealed class MessageNotFoundException : NotFoundException
    {   // 404
        public MessageNotFoundException(Guid id) : base($"The message with id: {id} could not found.")
        {
            
        }
    }
}
