using BulletinBoard.BL.User.Entities;

namespace BulletinBoard.Service.Controllers.Entities;

public class UsersListResponse
{
    public List<UserModel> Users { get; set; }
}