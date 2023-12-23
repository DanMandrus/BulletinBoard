using AutoMapper;
using BulletinBoard.Service.Mapper;

namespace BulletinBoard.BL.UnitTests.Mapper;

public static class MapperHelper
{
    static MapperHelper()
    {
        var config = new MapperConfiguration(x => x.AddProfile(typeof(UsersServiceProfile)));
        Mapper = new AutoMapper.Mapper(config);
    }

    public static IMapper Mapper { get; }
}