namespace BulletinBoard.Service.Controllers.Entities;

public class UsersFilter
{
    public int? MinimumAge { get; set; }
    public int? MaximumAge { get; set; }
    public bool? Sex { get; set; }
}