using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.EFCore.Lobbies
{
    public class LobbyRepository : RepositoryBase<Lobby> , ILobbyRepository
    {
        public LobbyRepository(RepositoriesContext context) : base(context)
        {
            
        }

        public void CreateOneLobby(Lobby Lobby) => Create(Lobby);
        

        public void DeleteOneLobby(Lobby Lobby) => Delete(Lobby);
        

        public async Task<IEnumerable<Lobby>> GetAllLobbiesAsync(bool trackChanges)
        {
            var Lobbies = await FindAll(trackChanges).ToListAsync();
            return Lobbies;
        }

        public async Task<Lobby> GetOneLobbyByIdAsync(Guid id, bool trackChanges) => await FindByCondition(ml => ml.LobbyId == id, trackChanges).FirstOrDefaultAsync();

        public void UpdateOneLobby(Lobby Lobby) => Update(Lobby);
        
    }

}
