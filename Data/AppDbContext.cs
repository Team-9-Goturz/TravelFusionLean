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
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelStay> HotelStays { get; set; }
        public DbSet<TravelPackage> TravelPackages { get; set; }

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

                entity.HasOne(f => f.DepartureFromAirport)
                      .WithMany()
                      .HasForeignKey(f => f.DepartureFromAirportId);

                entity.HasOne(f => f.Currency)
                      .WithMany()
                      .HasForeignKey(f => f.CurrencyId);
            });

            // === HotelStay ===
            modelBuilder.Entity<HotelStay>(entity =>
            {
                entity.ToTable("HotelStay");
                entity.HasKey(hs => hs.Id);

                entity.HasOne(hs => hs.Hotel)
                      .WithMany()
                      .HasForeignKey(hs => hs.HotelId);

                entity.HasOne(hs => hs.Currency)
                      .WithMany()
                      .HasForeignKey(hs => hs.CurrencyId);
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

            // === Øvrige tabeller ===
            modelBuilder.Entity<Airport>().ToTable("Airport");
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<Currency>().ToTable("Currency");
            modelBuilder.Entity<Hotel>().ToTable("Hotel");
        }
    }
}
