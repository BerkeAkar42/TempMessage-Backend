using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.EFCore
{
    public class LobbyUsersRepository : RepositoryBase<LobbyUsers> , ILobbyUsersRepository
    {
        public LobbyUsersRepository(RepositoriesContext context) : base(context)
        {
            
        }

        public void CreateOneLobbyUser(LobbyUsers lobbyUser) => Create(lobbyUser);

        public void DeleteOneLobbyUser(LobbyUsers lobbyUser) => Delete(lobbyUser);
        

        public async Task<IEnumerable<LobbyUsers>> GetAllLobbyUsersAsync(bool trackChanges)
        {
            var lobbies = await FindAll(trackChanges).ToListAsync();
            return lobbies;
        }

        public async Task<LobbyUsers> GetOneLobbyUserByIdAsync(Guid id, bool trackChanges)
        {
            var lobbyUser = await FindByCondition(lb => lb.LobbyUsersId == id, trackChanges).FirstOrDefaultAsync();
            return lobbyUser;
        }
        

        public void UpdateOneLobbyUser(LobbyUsers lobbyUser) => Update(lobbyUser);
        
    }

}
