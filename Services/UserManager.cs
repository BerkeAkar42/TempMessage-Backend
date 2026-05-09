using AutoMapper;
using Entities.DataObjectModels.User;
using Entities.Exceptions.UserExceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public UserManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<UserAuthDto> CreateOneUserAsync(UserDtoForInsertion user)
        {
            var newUser = _mapper.Map<User>(user);

            //Token üretmemiz gerek.

            _manager.User.CreateOneUser(newUser);
            await _manager.SaveAsync();
            return _mapper.Map<UserAuthDto>(newUser);



            /*
            // 1. Mapping: DTO'dan Entity'ye (Hatırla: Constructor'da AccessKey ve UserId zaten oluşuyor!)
            var newUser = _mapper.Map<User>(userDto);

            // 2. Kullanıcıyı veritabanına ekle ve kaydet
            _manager.User.CreateOneUser(newUser);
            await _manager.SaveAsync();

            // 3. Token Üretimi (AuthenticationService üzerinden)
            // Burada odaların süresine göre bir expire date hesaplayacağız
            var token = _authService.GenerateJwtToken(newUser);

            // 4. Paketleme: Entity -> UserAuthDto
            var authDto = _mapper.Map<UserAuthDto>(newUser);

            // 5. Token'ı pakete ekliyoruz (Frontend LocalStorage'a atsın diye)
            authDto.Token = token;

            return authDto;
            */
        }

        //Bir kullanıcı siler.
        public async Task DeleteOneUserAsync(Guid id, bool trackChanges)
        {
            var user = await _manager.User.GetOneUserByIdAsync(id, trackChanges); //trackChanges --> true

            if (user is null)
                throw new UserNotFoundException(id);

            _manager.User.DeleteOneUser(user);

            await _manager.SaveAsync();
        }

        //Tüm kullanıcıları getirir
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(bool trackChanges)
        {
            var users = _manager.User.GetAllUsersAsync(trackChanges);
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
        }
    }
}
