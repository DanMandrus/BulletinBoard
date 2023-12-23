using BulletinBoard.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.DataAccess;

public class BulletinBoardDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
    public DbSet<AnnouncementEntity> Announcements { get; set; }

    public BulletinBoardDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnnouncementEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<AnnouncementEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<AnnouncementEntity>().HasOne(x => x.Images)
            .WithMany(x => x.Announcements)
            .HasForeignKey(x => x.ImageId);
        modelBuilder.Entity<AnnouncementEntity>().HasOne(x => x.Users)
           .WithMany(x => x.Announcements)
           .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<ImageEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<ImageEntity>().HasIndex(x => x.ExternalId).IsUnique();

        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.ExternalId).IsUnique();
    }
}