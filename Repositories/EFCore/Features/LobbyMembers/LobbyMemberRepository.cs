using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using System.Linq.Expressions;

namespace Repositories.EFCore.Features.LobbyMembers
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

        /// <summary>
        /// User o lobinin üyesi mi?
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lobbyId"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        public Task<bool> IsUserMemberOfLobbyAsync(Guid userId, Guid lobbyId, bool trackChanges) =>
            FindByCondition(lm => lm.LobbyId == lobbyId && lm.UserId == userId, trackChanges)
            .AnyAsync();

        /// <summary>
        /// User'ın aktif tüm lobilerini getirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LobbyMember>> GetActiveMembershipsByUserIdAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(lm => lm.UserId == userId && lm.Lobby.IsActive, trackChanges)
                .Include(lm => lm.Lobby)
                .ToListAsync();


        public void UpdateOneLobbyMember(LobbyMember lobbyMember) => Update(lobbyMember);


        /// <summary>
        /// Bir lobbideki user entity'lerini listeler.
        /// </summary>
        /// <param name="lobbyId"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<User>> GetLobbyParticipantsByLobbyIdAsync(Guid lobbyId, bool trackChanges) =>
            //Mesajlaşma ekranında sağ tarafta  mini bir yer açıp oraya listeletebiliriz.
            await FindByCondition(ml => ml.LobbyId == lobbyId, trackChanges)
                //.Include(ml => ml.User)
                .Select(ml=> ml.User)
                .ToListAsync();



    }

}
