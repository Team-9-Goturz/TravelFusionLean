using Microsoft.EntityFrameworkCore;
using Shared.Models;
using TravelFusionLean.Models;

namespace TravelFusionLean.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<TravelPackage> TravelPackages { get; set; }


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
        });

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserRole)
            .WithMany() 
            .HasForeignKey(u => u.UserRoleId)
            .IsRequired();


        modelBuilder.Entity<User>()
            .HasOne(u => u.Contact)
            .WithOne()
            .HasForeignKey<User>(u => u.ContactId)
            .IsRequired();

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contact"); 
        });

        // === TravelPackage ===
        modelBuilder.Entity<TravelPackage>(entity =>
        {
            entity.ToTable("TravelPackage");
            entity.HasKey(tp => tp.Id);

            entity.HasOne(tp => tp.OutboundFlight)
                  .WithMany()
                  .HasForeignKey(tp => tp.OutboundFlightId);

            entity.HasOne(tp => tp.InboundFlight)
                  .WithMany()
                  .HasForeignKey(tp => tp.InboundFlightId);

            entity.HasOne(tp => tp.HotelStay)
                  .WithMany()
                  .HasForeignKey(tp => tp.HotelStayId);

            entity.HasOne(tp => tp.ToHotelTransfer)
                  .WithMany()
                  .HasForeignKey(tp => tp.ToHotelTransferId);

            entity.HasOne(tp => tp.FromHotelTransfer)
                  .WithMany()
                  .HasForeignKey(tp => tp.FromHotelTransferId);
        });
    }


}

