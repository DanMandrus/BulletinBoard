using BulletinBoard.BL.User.Entities;

namespace BulletinBoard.BL.User;

public interface IUsersManager
{
    UserModel CreateUser(CreateUserModel model);
    UserModel DeleteUser(CreateUserModel model);
    UserModel UpdateUser(CreateUserModel model);
}