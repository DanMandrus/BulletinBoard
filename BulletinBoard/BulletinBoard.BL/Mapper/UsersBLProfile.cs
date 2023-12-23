using AutoMapper;
using BulletinBoard.BL.User.Entities;
using BulletinBoard.DataAccess.Entities;

namespace BulletinBoard.BL.Mapper;

public class UsersBLProfile : Profile
{
    public UsersBLProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(x => x.Id, y => y.MapFrom(src => src.ExternalId))
            .ForMember(x => x.FullName, y => y.MapFrom(src => $"{src.FirstName} {src.SecondName} {src.Patronymic}"));

        CreateMap<CreateUserModel, UserEntity>()
            .ForMember(x => x.Id, y => y.Ignore())
            .ForMember(x=>x.ExternalId, y=>y.Ignore())
            .ForMember(x=>x.ModificationTime, y=>y.Ignore())
            .ForMember(x=>x.CreationTime, y=>y.Ignore());
    }
}