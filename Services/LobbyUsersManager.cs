using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LobbyUsersManager : ILobbyUsersService
    {
        private readonly IRepositoryManager _manager;

        public LobbyUsersManager(IRepositoryManager manager)
        {
            _manager = manager;
        }
    }
}
