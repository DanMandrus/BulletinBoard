using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.DataAccess.Entities;

[Table("image")]

public class ImageEntity : BaseEntity
{
    public string ImageName { get; set; }
    public string Link { get; set; }
}
