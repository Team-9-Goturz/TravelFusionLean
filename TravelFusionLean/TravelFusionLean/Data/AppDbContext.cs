using Microsoft.EntityFrameworkCore;
using TravelFusionLean.Models;

namespace TravelFusionLean.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");

            entity.HasKey(ur => ur.Id);
            entity.Property(ur => ur.Name)
               .IsRequired();

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username).IsRequired();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.PasswordSalt).IsRequired();
            entity.Property(u => u.Email).IsRequired();

            //entity.HasOne(u => u.UserRole)
            //    .WithMany(ur => ur.Users)
            //    .HasForeignKey(u => u.UserRoleId)
            //    .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

