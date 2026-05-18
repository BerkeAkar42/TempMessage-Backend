using AutoMapper;
using Entities.Dtos.Lobby;
using Entities.Dtos.User;
using Entities.Exceptions.Lobby;
using Entities.Exceptions.UserExceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Services.Common;
using Services.Features.Authentication;


namespace Services.Features.Lobbies
{   //Lobby oluşturulurken aynı zamanda kullanıcı da oluşturulduğu için tüm işlemler burada yapılacak.
    public class LobbyManager : ILobbyService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IAuthenticationService _authenticationService;

        public LobbyManager(IRepositoryManager manager, IMapper mapper, ILoggerService logger, IAuthenticationService authenticationService)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
            _authenticationService = authenticationService;
        }


        /// <summary>
        /// Yeni lobby açma metotu.
        /// </summary>
        /// <param name="lobbyDto">Lobby bilgilerini içerir</param>
        /// <param name="userDto">User bilgilerini içerir</param>
        /// <param name="userIdFromToken">User token'a sahipse id döner. Yeni oluşturulan odalara göre user yaşam süresi hesaplanır</param>
        /// <returns>LobbyAuthResponseDto (LobbyDto, UserDto)</returns>
        /// <exception cref="UserNotFoundException">Kullanıcı bulunamdı hatası</exception>
        public async Task<LobbyAuthResponseDto> CreateOneLobbyAsync(LobbyDtoForInsertion lobbyDto, UserDtoForInsertion userDto, Guid? userIdFromToken)
        {
            //Controllerda çağırırken şöyle kullan:
            //// 1. Kim bu adam? (Authentication servisine soruyoruz)
            //var userIdFromToken = _service.AuthenticationService.GetUserIdFromCurrentContext();

            //// 2. Lobi kur (Logic yine LobbyService'de kalıyor)
            //var result = await _service.LobbyService.CreateOneLobbyAsync(lobi için gerekli parametreler, tokenId = userIdFromToken);

            //return Ok(result);

            User user;

            //Token varsa bilgilerini al
            if (userIdFromToken.HasValue)
            {
                user = await _manager.User.GetOneUserByIdAsync(userIdFromToken.Value, false);

                if (user is null)
                    throw new UserNotFoundException(userIdFromToken.Value);
            }
            else
            {
                //Token yoksa yeni kullanıcı oluştur
                user = _mapper.Map<User>(userDto);
                _manager.User.CreateOneUser(user);
                _logger.LogInfo($"User Created | ID: {user.UserId}");
            }


            //Lobby oluştur
            var newLobby = _mapper.Map<Lobby>(lobbyDto);
            _manager.Lobby.CreateOneLobby(newLobby);


            //Lobby Member ilişkisini kur
            var lobbyMember = new LobbyMember
            {
                LobbyId = newLobby.LobbyId,
                UserId = user.UserId,
                IsAdmin = true //Lobi sahipliğini bildirir.
            };
            _manager.LobbyMember.CreateOneLobbyMember(lobbyMember);
            await _manager.SaveAsync(); //Db kaydet

            
            _logger.LogInfo($"Lobby Created: Name: {newLobby.Name} - ID: {newLobby.LobbyId} | Owner: {user.NickName} - ID: {user.UserId}");
            
            //Token üretimi yapılarak yanıt döndürür
            return await PrepareLobbyAuthResponse(user, newLobby);
        }



        /// <summary>
        /// Lobby'e katılma metotu
        /// </summary>
        /// <param name="lobbyId">Aktif olan lobinin id'si</param>
        /// <param name="userDto">User bilgisi yoksa, user kayıt için alınan bilgiler</param>
        /// <param name="userIdFromToken">User token'a sahipse id döner. Yeni oluşturulan odalara göre user yaşam süresi hesaplanır</param>
        /// <returns>LobbyAuthResponseDto (LobbyDto, UserDto)</returns>
        /// <exception cref="LobbyNotFoundException">Lobby bulunamadı hatası</exception>
        /// <exception cref="UserNotFoundException">User bulunaamadı hatası</exception>
        public async Task<LobbyAuthResponseDto> JoinLobbyAsync(Guid lobbyId, UserDtoForInsertion userDto, Guid? userIdFromToken)
        {
            //Controllerda çağırırken şöyle kullan:
            //// 1. Kim bu adam? (Authentication servisine soruyoruz)
            //var userIdFromToken = _service.AuthenticationService.GetUserIdFromCurrentContext();

            //// 2. Lobi kur (Logic yine LobbyService'de kalıyor)
            //var result = await _service.LobbyService.CreateOneLobbyAsync(lobi için gerekli parametreler, tokenId = userIdFromToken);

            //return Ok(result);

            var lobby = await _manager.Lobby.GetOneLobbyByIdAsync(lobbyId, false);
            if (lobby is null)
                throw new LobbyNotFoundException(lobbyId);

            User user;

            //Token bilgisinden kullanıcı kontrolü
            if (userIdFromToken.HasValue)
            {
                // Token varsa: Kullanıcıyı getir
                user = await _manager.User.GetOneUserByIdAsync(userIdFromToken.Value, false);
                if (user is null) 
                    throw new UserNotFoundException(userIdFromToken.Value);

                //Kullanıcı zaten bu lobinin üyesi mi? (Mükerrer kaydı önleme)
                var isAlreadyMember = await _manager.LobbyMember.IsUserMemberOfLobbyAsync(user.UserId, lobby.LobbyId, false);

                if (isAlreadyMember)
                {
                    _logger.LogInfo($"User {user.NickName} is already a member of Lobby {lobbyId}.");
                    // Eğer zaten üyeyse sadece token'ını yenileyip döndür.
                    return await PrepareLobbyAuthResponse(user, lobby);
                }
            }
            else
            {
                user = _mapper.Map<User>(userDto);
                _manager.User.CreateOneUser(user);
                _logger.LogInfo($"New guest user created for lobby join | Id: {user.UserId}");
            }



            //Kullanıcıyı lobiye üye olarak ekle
            var lobbyMember = new LobbyMember
            {
                LobbyId = lobbyId,
                UserId = user.UserId,
                IsAdmin = false //kullanıcı sadece üyedir!
            };
            _manager.LobbyMember.CreateOneLobbyMember(lobbyMember);
            await _manager.SaveAsync(); //db ye kayıt

            _logger.LogInfo($"User {user.UserId} joined Lobby: {lobby.LobbyId}");

            return await PrepareLobbyAuthResponse(user, lobby);
        }


        /// <summary>
        /// Lobby oluşturma ve Lobby katılma metotları için response metot.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lobby"></param>
        /// <returns></returns>
        private async Task<LobbyAuthResponseDto> PrepareLobbyAuthResponse(User user, Lobby lobby)
        {
            //Hesap yaşam süresi hesapla
            int finalDuration = await GetMaxLobbyValidityPeriodAsync(user.UserId, lobby);

            //Token üret
            var token = _authenticationService.GenerateToken(user, finalDuration);

            _logger.LogInfo($"Token generated for User ID: {user.UserId} | Expires in {finalDuration} minutes.");


            //Map'lenmiş veri dön
            return new LobbyAuthResponseDto
            {
                Lobby = _mapper.Map<LobbyDto>(lobby),
                User = _mapper.Map<UserAuthDto>(user) with { Token = token }
            };
        }


        /// <summary>
        /// Lobby süresi hesaplama metotdu.
        /// Kullanıcının aktif olan tüm Lobby'lerini getirir. İçlerinden en uzun süreye sahip olan metotun süresini bellekte tutar.
        /// Metot, token üretmek/yenilemek için kullanılır.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentLobbyValidity"></param>
        /// <returns></returns>
        private async Task<int> GetMaxLobbyValidityPeriodAsync(Guid userId, Lobby currentLobby)
        {
            //Kullanıcının halihazırda üye olduğu AKTİF lobileri getir
            var activeMemberships = await _manager.LobbyMember.GetActiveMembershipsAsync(userId, false);

            //Mevcut lobileri ve yeni katılacağı lobiyi tek bir listede topla
            //Eğer kullanıcı zaten bu lobideyse listede mükerrer olmasın diye kontrol et
            var allLobbies = activeMemberships.Select(lm => lm.Lobby).ToList();

            if (!allLobbies.Any(l => l.LobbyId == currentLobby.LobbyId))
            {
                allLobbies.Add(currentLobby);
            }

            //Tüm lobiler içinden en geç kapanacak olanın kalan süresini hesapla
            var maxRemainingMinutes = allLobbies
                .Select(l =>
                {
                    // Bitiş tarihi = Kuruluş + Toplam Ömür
                    var expireDate = l.CreateDate.AddMinutes(l.ValidityPeriod);
                    // Kalan süre = Bitiş - Şu an
                    var remaining = (expireDate - DateTime.UtcNow).TotalMinutes;
                    return (int)Math.Max(0, remaining);
                })
                .Max();

            //User yaşam süresi için pay bırak
            return maxRemainingMinutes + 5;
        }



        public async Task DeleteOneLobbyAsync(Guid id, bool trackChanges)
        {
            var lobby = await _manager.Lobby.GetOneLobbyByIdAsync(id, trackChanges);

            if (lobby is null)
                throw new LobbyNotFoundException(id);

            _manager.Lobby.DeleteOneLobby(lobby);
            await _manager.SaveAsync();

            _logger.LogInfo($"Lobby Deleted | ID: {lobby.LobbyId}");
        }



        public async Task UpdateOneLobbyAsync(LobbyDtoForUpdate lobbyDto, bool trackChanges)
        {
            var currentLobby = await _manager.Lobby.GetOneLobbyByIdAsync(lobbyDto.LobbyId, trackChanges);

            if (currentLobby is null)
                throw new LobbyNotFoundException(lobbyDto.LobbyId);

            _mapper.Map(lobbyDto, currentLobby);
            _manager.Lobby.UpdateOneLobby(currentLobby);
            await _manager.SaveAsync();

            _logger.LogInfo($"Lobby Updated | ID: {currentLobby.LobbyId}");
        }
    }
}
