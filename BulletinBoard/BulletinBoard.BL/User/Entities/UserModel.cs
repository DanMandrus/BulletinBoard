namespace BulletinBoard.BL.User.Entities;

public class UserModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public int Sex { get; set; }
    public int Age { get; set; }
}