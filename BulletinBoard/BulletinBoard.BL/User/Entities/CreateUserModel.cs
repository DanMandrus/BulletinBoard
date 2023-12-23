namespace BulletinBoard.BL.User.Entities;

public class CreateUserModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Sex { get; set; }
    public DateTime Birthday { get; set; }
    public string Position { get; set; }
}