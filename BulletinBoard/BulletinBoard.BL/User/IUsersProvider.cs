using BulletinBoard.BL.User.Entities;

namespace BulletinBoard.BL.User;

public interface IUsersProvider
{
    IEnumerable<UserModel> GetUsers(UsersModelFilter modelFilter = null);
    UserModel GetUserInfo(Guid UserId);
}