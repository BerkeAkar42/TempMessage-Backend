using AutoMapper;
using Entities.Dtos.User;
using Entities.Exceptions.UserExceptions;
using Entities.Models;
using Repositories.Context;
using Services.Common;
using Services.Features.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Users
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggerService _logger;

        public UserManager(IRepositoryManager manager, IMapper mapper, IAuthenticationService authenticationService, ILoggerService logger)
        {
            _manager = manager;
            _mapper = mapper;
            _authenticationService = authenticationService;
            _logger = logger;
        }

        //Bir kullanıcı oluşturur
        public async Task<UserAuthDto> CreateOneUserAsync(UserDtoForInsertion user)
        {
            //Her halükarda bir oda sonucunda hesap oluşturulacağı kanaatine vardık. Bu yüzden user --> CreateOneUserAsync(UserDtoForInsertion user, int expireMinutes) olacak şekilde ayarlanmalı.
            //Bu metot LobbyMember logic'inde kullanıcı eklenirken çağırılacak.

            var newUser = _mapper.Map<User>(user);

            _manager.User.CreateOneUser(newUser);
            await _manager.SaveAsync();

            _logger.LogInfo($"User Registration Successful | ID: {newUser.UserId} | Nickname: {newUser.NickName}");

            //Token üretme
            var token = _authenticationService.GenerateToken(newUser); //Kullanıcının katıldığı lobby'nin süresi şu an belli olmadığından appsetting.json daki süre baz alındı.

            var userAuthDto = _mapper.Map<UserAuthDto>(newUser);

            userAuthDto.Token = token;

            return userAuthDto;
        }

        //Bir kullanıcı siler.
        public async Task DeleteOneUserAsync(Guid id, bool trackChanges)
        {
            var user = await _manager.User.GetOneUserByIdAsync(id, trackChanges); //trackChanges --> true

            if (user is null)
                throw new UserNotFoundException(id);

            _manager.User.DeleteOneUser(user);

            await _manager.SaveAsync();
            _logger.LogInfo($"User Deleted | ID: {user.UserId}");
        }

        //Tüm kullanıcıları getirir
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(bool trackChanges)
        {
            var users = await _manager.User.GetAllUsersAsync(trackChanges);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        //Tek bir kullanıcıyı getirir
        public async Task<UserDto> GetOneUserByIdAsync(Guid id, bool trackChanges)
        {
            var user = await _manager.User.GetOneUserByIdAsync(id, trackChanges);

            if (user is null)
                throw new UserNotFoundException(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateOneUserAsync(Guid id, UserDtoForUpdate user, bool trackChanges)
        {
            var currentUser = await _manager.User.GetOneUserByIdAsync(id, trackChanges); //trackChanges --> true olmalı. Veri değiştirilecek.

            if (currentUser is null)
                throw new UserNotFoundException(id);

            _mapper.Map(user, currentUser); //gelen user nesnemi al mevcut user'ımın üzerine yaz.

            _manager.User.UpdateOneUser(currentUser);

            await _manager.SaveAsync(); //Değişiklikleri db'ye kaydet.
            _logger.LogInfo($"User Updated | ID: {currentUser.UserId}");
        }
    }
}
