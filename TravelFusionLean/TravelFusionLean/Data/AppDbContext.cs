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

            entity.HasKey(u => u.id);
            entity.Property(u => u.username).IsRequired();
            entity.Property(u => u.passwordHash).IsRequired();
            entity.Property(u => u.passwordSalt).IsRequired();
            entity.Property(u => u.email).IsRequired(); 
        });
    }

}

