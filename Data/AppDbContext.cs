using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using TravelFusionLean.Models;

namespace Data
{
    /// <summary>
    /// Repræsenterer Entity Framework Core databasekontekst og definerer tabeller og struktur.
    /// </summary>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// DbSet til UserRoles-tabellen.
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// DbSet til Users-tabellen.
        /// </summary>
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; } 


        /// <summary>
        /// Konfigurerer tabelstruktur og relationer mellem entiteter.
        /// </summary>
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

                // Contacts
                modelBuilder.Entity<User>()
                     .HasOne(u => u.Contact)
                    .WithOne()
                    .HasForeignKey<User>(u => u.ContactId)
                    .OnDelete(DeleteBehavior.Cascade); // valgfrit
            });

            modelBuilder.Entity<Contact>().ToTable("Contact");
        }
    }
}
