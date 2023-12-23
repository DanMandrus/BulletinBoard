using AutoMapper;
using BulletinBoard.BL.User.Entities;
using BulletinBoard.Service.Controllers.Entities;

namespace BulletinBoard.Service.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<UsersFilter, UsersModelFilter>();
        CreateMap<CreateUsersRequest, CreateUsersModel>(); 
    }
}