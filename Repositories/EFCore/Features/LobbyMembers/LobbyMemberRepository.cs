using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using System.Linq.Expressions;

namespace Repositories.EFCore.Features.LobbyMembers
{
    public class LobbyMemberRepository : RepositoryBase<LobbyMember>, ILobbyMemberRepository
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

        /// <inheritdoc />
        public Task<bool> IsUserMemberOfLobbyAsync(Guid userId, Guid lobbyId, bool trackChanges) =>
            FindByCondition(lm => lm.LobbyId == lobbyId && lm.UserId == userId, trackChanges)
            .AnyAsync();

        /// <inheritdoc />
        public async Task<IEnumerable<LobbyMember>> GetActiveMembershipsByUserIdAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(lm => lm.UserId == userId && lm.Lobby.IsActive, trackChanges)
                .Include(lm => lm.Lobby)
                .Include(lm => lm.User)
                .ToListAsync();


        public void UpdateOneLobbyMember(LobbyMember lobbyMember) => Update(lobbyMember);


        /// <inheritdoc />
        public async Task<IEnumerable<User>> GetLobbyParticipantsByLobbyIdAsync(Guid lobbyId, bool trackChanges) =>
            //Mesajlaşma ekranında sağ tarafta  mini bir yer açıp oraya listeletebiliriz.
            await FindByCondition(ml => ml.LobbyId == lobbyId, trackChanges)
                //.Include(ml => ml.User)
                .Select(ml => ml.User)
                .OrderBy(u => u.NickName)
                .ToListAsync();


    }

}
