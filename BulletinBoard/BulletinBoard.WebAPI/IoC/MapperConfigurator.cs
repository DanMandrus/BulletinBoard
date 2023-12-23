using BulletinBoard.BL.Mapper;
using BulletinBoard.Service.Mapper;

namespace BulletinBoard.Service.IoC;

public static class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UsersBLProfile>();
            config.AddProfile<UsersServiceProfile>();
        });
    }
}