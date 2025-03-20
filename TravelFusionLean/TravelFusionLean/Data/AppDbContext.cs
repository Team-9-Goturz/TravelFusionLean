using Microsoft.EntityFrameworkCore;
using TravelFusionLean.Models;

namespace TravelFusionLean.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserRole> UserRole { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(ur => ur.Id);
            entity.Property(ur => ur.Name)
                .IsRequired();

        });
    }

}

