using AutoMapper;
using BulletinBoard.BL.Helpers;
using BulletinBoard.BL.User.Entities;
using BulletinBoard.DataAccess;
using BulletinBoard.DataAccess.Entities;

namespace BulletinBoard.BL.User;

public class UsersProvider : IUsersProvider
{
    private readonly IRepository<UserEntity> _UserRepository;
    private readonly IMapper _mapper;

    public UsersProvider(IRepository<UserEntity> UsersRepository, IMapper mapper, int minimumUserAge)
    {
        _UserRepository = UsersRepository;
        _mapper = mapper;
    }

    public IEnumerable<UserModel> GetUsers(UsersModelFilter modelFilter = null)
    {
        var minimumAge = modelFilter?.MinimumAge;
        var maximumAge = modelFilter?.MaximumAge;
        var sex = modelFilter?.Sex;

        var currentDate = DateTime.UtcNow;

        var Users = _UserRepository.GetAll(x => (
            minimumAge == null ||
            AgeHelper.GetAge(x.Birthday) > minimumAge)); //add other filters

        return _mapper.Map<IEnumerable<UserModel>>(Users);
    }

    public UserModel GetUserInfo(Guid UserId)
    {
        var User = _UserRepository.GetById(UserId); //return null if not exists
        if (User is null)
        {
            throw new ArgumentException("User not found.");
        }

        return _mapper.Map<UserModel>(User);
    }
}