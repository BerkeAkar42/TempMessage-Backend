using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MessageLobbyManager : IMessageLobbyService
    {
        private readonly IRepositoryManager _manager;

        public MessageLobbyManager(IRepositoryManager manager)
        {
            _manager = manager;
        }
    }
}
