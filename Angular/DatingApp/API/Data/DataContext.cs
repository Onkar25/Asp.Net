using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }

    public DbSet<UserLike> Likes { get; set; }

    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserLike>()
        .HasKey(k => new { k.SourceUserId, k.TargetUserId });

        builder.Entity<UserLike>()
        .HasOne(s => s.SourceUser)
        .WithMany(m => m.LikedUser)
        .HasForeignKey(h => h.SourceUserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserLike>()
        .HasOne(s => s.TargetUser)
        .WithMany(m => m.LikedByUser)
        .HasForeignKey(h => h.TargetUserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Message>()
        .HasOne(x=> x.Recipient)
        .WithMany(x=>x.MessageReceived)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Message>()
        .HasOne(x=> x.Sender)
        .WithMany(x=>x.MessageSend)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
