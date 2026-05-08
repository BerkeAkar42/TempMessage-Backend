using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MessageManager : IMessageService
    {
        private readonly IRepositoryManager _manager;

        public MessageManager(IRepositoryManager manager)
        {
            _manager = manager;
        }
    }
}
