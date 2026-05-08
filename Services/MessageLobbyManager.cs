using AutoMapper;
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
        private readonly IMapper _mapper;

        public MessageLobbyManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
    }
}
