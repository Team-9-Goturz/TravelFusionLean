using Microsoft.EntityFrameworkCore;
using Shared.Models; 
using TravelFusionLean.Models;

namespace Data
{
    /// <summary>
    /// Repræsenterer Entity Framework Core databasekontekst og definerer tabeller og struktur.
    /// Indeholder DbSet's for alle entiteter i TravelFusion-databasen.
    /// </summary>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
       

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }


        /// <summary>
        /// Konfigurerer tabelstruktur, constraints og relationer mellem entiteter,
        /// så det stemmer overens med den eksisterende SQL Server-database.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === Brugerroller ===
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
                entity.HasKey(ur => ur.Id);
                entity.Property(ur => ur.Name).IsRequired().HasMaxLength(128);
            });

            // === Brugere ===
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(128);
                entity.Property(u => u.PasswordSalt).IsRequired().HasMaxLength(128);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(128);

                entity.HasOne(u => u.Contact)
                      .WithMany()
                      .HasForeignKey(u => u.ContactId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.UserRole)
                      .WithMany()
                      .HasForeignKey(u => u.UserRoleId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // === Flight-relateret konfiguration ===
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("Flight");
                entity.HasKey(f => f.Id);

                entity.HasOne(f => f.ArrivalAtAirport)
                      .WithMany()
                      .HasForeignKey(f => f.ArrivalAtAirportId);

                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired();
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.PasswordSalt).IsRequired();
                entity.Property(u => u.Email).IsRequired();
            });
        }
    }
}
