using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.EFCore.LobbyMembers
{
    public class LobbyMemberRepository : RepositoryBase<LobbyMember> , ILobbyMemberRepository
    {
        public LobbyMemberRepository(RepositoriesContext context) : base(context)
        {
            
        }

        public void CreateOneLobbyMember(LobbyMember lobbyMember) => Create(lobbyMember);

        public void DeleteOneLobbyMember(LobbyMember lobbyMember) => Delete(lobbyMember);
        

        public async Task<IEnumerable<LobbyMember>> GetAllLobbyMembersAsync(bool trackChanges)
        {
            var lobbies = await FindAll(trackChanges).ToListAsync();
            return lobbies;
        }

        public async Task<LobbyMember> GetOneLobbyMemberByIdAsync(Guid id, bool trackChanges)
        {
            var lobbyMember = await FindByCondition(lb => lb.LobbyMemberId == id, trackChanges).FirstOrDefaultAsync();
            return lobbyMember;
        }
        

        public void UpdateOneLobbyMember(LobbyMember lobbyMember) => Update(lobbyMember);
        
    }

}
