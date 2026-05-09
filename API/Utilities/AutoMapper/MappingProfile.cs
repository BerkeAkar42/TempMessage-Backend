using AutoMapper;
using Entities.DataObjectModels.LobbyUsers;
using Entities.DataObjectModels.Message;
using Entities.DataObjectModels.MessageLobby;
using Entities.DataObjectModels.User;
using Entities.Models;

namespace API.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Kaynak, Hedef>();
            // --> Mantık: Veritabanından gelen User entity’sini al, içindeki verileri UserDto’ya boşalt. Bu tek yönlü bir köprüdür. Sadece Entity'den DTO'ya gidiş vardır.

            /*
             - .ReverseMap() -> mapleme işlemini çift yönlğ uygulanmasını sağlayan fonksiyon.
             - Kullanıcıdan veri okurken kayıt işlemi olmayacağından .ReverseMap() kullanılmaz.
             - Genel DTO'lar hem veri okumada hem de geri döndürmede kullanılıyorsa .ReverseMap() kullanılır.
             */

            CreateMap<User, UserDto>(); //GET
            CreateMap<User, UserAuthDto>(); //Doğrulama İşlemleri
            CreateMap<UserDtoForInsertion, User>(); //POST
            CreateMap<UserDtoForUpdate, User>().ReverseMap(); //PUT


            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.User.NickName)); //GET
            CreateMap<MessageDtoForUpdate, Message>().ReverseMap(); //PUT
            CreateMap<MessageDtoForInsertion, Message>(); //POST


            CreateMap<MessageLobby, MessageLobbyDto>(); //GET
            CreateMap<MessageLobbyDtoForInsertion, MessageLobby>(); //POST
            CreateMap<MessageLobbyDtoForUpdate, MessageLobby>().ReverseMap(); //PUT


            CreateMap<LobbyUsers, LobbyUsersDto>()
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.User.NickName)); //GET
            CreateMap<LobbyUsersDtoForInsertion, LobbyUsers>(); //POST
        }
    }
}
