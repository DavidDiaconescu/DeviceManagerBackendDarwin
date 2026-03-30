using DeviceManagement.API.Models.Entities;
using DeviceManagement.API.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Device> Devices => Set<Device>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasIndex(d => new { d.Name, d.Manufacturer })
                  .IsUnique();

            entity.Property(d => d.Type)
                  .HasConversion(
                      t => t.ToString(),
                      t => Enum.Parse<DeviceType>(t));

            entity.HasOne(d => d.AssignedUser)
                  .WithMany()
                  .HasForeignKey(d => d.AssignedUserId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email)
                  .IsUnique();
        });
    }
}
