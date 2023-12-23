using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.DataAccess.Entities;

[Table("announcements")]
public class AnnouncementEntity : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public virtual ICollection<UserEntity> Users { get; set; }
    public virtual ICollection<ImageEntity> Images { get; set; }
}
