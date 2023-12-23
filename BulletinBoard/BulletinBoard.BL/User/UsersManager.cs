using AutoMapper;
using BulletinBoard.BL.Helpers;
using BulletinBoard.BL.User.Entities;
using BulletinBoard.DataAccess;
using BulletinBoard.DataAccess.Entities;

namespace BulletinBoard.BL.User;

public class UsersManager : IUsersManager
{
    private readonly IRepository<UserEntity> _UsersRepository;
    private readonly IMapper _mapper;
    
    public UsersManager(IRepository<UserEntity> UsersRepository, IMapper mapper)
    {
        _UsersRepository = UsersRepository;
        _mapper = mapper;
    }
    
    public UserModel CreateUser(CreateUserModel model)
    {
        if (AgeHelper.GetAge(model.Birthday) < 18)
        {
            throw new ArgumentException("Age must be greater than 18.");
        }

        var entity = _mapper.Map<UserEntity>(model);

        _UsersRepository.Save(entity); //id, modification, externalId

        return _mapper.Map<UserModel>(entity);
    }
}