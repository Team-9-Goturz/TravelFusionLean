using Microsoft.EntityFrameworkCore;
using Shared.Models;
using TravelFusionLean.Models;

namespace Data
{
    /// <summary>
    /// Repræsenterer Entity Framework Core databasekontekst og definerer tabeller og struktur.
    /// Indeholder DbSet's for alle entiteter i TravelFusion-databasen.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelStay> HotelStays { get; set; }
        public DbSet<TravelPackage> TravelPackages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Traveller> Travellers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Country> countries { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Konfigurerer tabelstruktur, constraints og relationer mellem entiteter,
        /// så det stemmer overens med den eksisterende SQL Server-database.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
                entity.HasKey(ur => ur.Id);
                entity.Property(ur => ur.Name).IsRequired().HasMaxLength(128);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(128);
                entity.Property(u => u.PasswordSalt).IsRequired().HasMaxLength(128);
                entity.Property(u => u.EmailForPasswordReset).IsRequired().HasMaxLength(128);
            });

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.UserRoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Contact)
                .WithOne()
                .HasForeignKey<User>(u => u.ContactId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); // ellers kunne en stored procedure der kørte dagligt fjerne alle kontakter der ikke er tilknyttet bruger eller aktiv booking 

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");
            });

            modelBuilder.Entity<Airport>().ToTable("Airport");
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("Hotel");
                entity.HasKey(h => h.Id);

              
            });

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
                // Mapper PriceAsDecimal til som en værdi-objekt
                entity.OwnsOne(p => p.Price, price =>
                {
                    price.Property(p => p.Amount)
                        .HasColumnName("PriceAmount")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    price.Property(p => p.Currency)
                        .HasColumnName("PriceCurrency")
                        .HasConversion<string>() // <-- Konverter enum til string
                        .IsRequired()
                        .HasMaxLength(3);
                });
            });

            modelBuilder.Entity<HotelStay>(entity =>
            {
                entity.ToTable("HotelStay");
                entity.HasKey(hs => hs.Id);

                entity.HasOne(hs => hs.Hotel)
                      .WithMany()
                      .HasForeignKey(hs => hs.HotelId);
                // Mapper PriceAsDecimal til som en værdi-objekt
                entity.OwnsOne(p => p.Price, price =>
                {
                    price.Property(p => p.Amount)
                        .HasColumnName("PriceAmount")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    price.Property(p => p.Currency)
                        .HasColumnName("PriceCurrency")
                        .HasConversion<string>() // <-- Konverter enum til string
                        .IsRequired()
                        .HasMaxLength(3);
                });

            });

            modelBuilder.Entity<TravelPackage>(entity =>
            {
                entity.ToTable("TravelPackage"); //Fortæller Entity framework at Travelpackage modellen skal mappes ned i en tabel kaldet "TravelPackage" i databasen 
                entity.HasKey(tp => tp.Id); // Fortæller Entity Framework at Id kolonnen i databasen er primærnøgle 

                entity.Property(tp => tp.Description).HasMaxLength(600); //fortæller Entity framework vores description kolonne maks kan være 600 tegn

                entity.HasOne(tp => tp.OutboundFlight) //Fortæller Entity framework at hver travelpackage har et udrejsefly 
                      .WithMany() // fortæller Entity framework at et udrejsefly kan være tilknyttet mange rejsepakker
                      .HasForeignKey(tp => tp.OutboundFlightId); //fortæller at outboundflightId kolonnen i tabellen er en fremmednøgle 

                entity.HasOne(tp => tp.InboundFlight) //en rejsepakke har et hjemrejsefly
                      .WithMany() // et hjemrejsefly kan optræde på flere rejsepakker
                      .HasForeignKey(tp => tp.InboundFlightId); //InboundFlightId er en fremmednøgle 

                entity.HasOne(tp => tp.HotelStay) // en rejsepakke har et hotelophold
                      .WithMany() //et hotelophold kan optræde på flere rejsepakker
                      .HasForeignKey(tp => tp.HotelStayId); //HotelStayId er en fremmednøgle 

                //entity.HasOne(tp => tp.ToHotelTransfer) // en rejsepakke indeholder en transport fra lufthavnen til hotellet (i forbindelse med udrejse) 
                //      .WithMany() //en transport fra lufthavn til hotel kan optræde på mange rejsepakker
                //      .HasForeignKey(tp => tp.ToHotelTransferId); //ToHotelTransferId er en fremmednøgle

                //entity.HasOne(tp => tp.FromHotelTransfer) //En rejsepakke indeholder en transport fra hotellet til lufthavnen (i forbindelse med hjemrejse)
                //      .WithMany() // en transport fra hotel til lufthavn kan være tilknyttet flere rejsepakker
                //      .HasForeignKey(tp => tp.FromHotelTransferId); //FromHotelTransferId er en fremmednøgle
                //      


                // Mapper PriceAsDecimal til som en værdi-objekt
                entity.OwnsOne(p => p.Price, price =>
                {
                    price.Property(p => p.Amount)
                        .HasColumnName("PriceAmount")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    price.Property(p => p.Currency)
                        .HasColumnName("PriceCurrency")
                        .HasConversion<string>() // <-- Konverter enum til string
                        .IsRequired()
                        .HasMaxLength(3);
                });
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.HasKey(b => b.Id);

                entity.Property(b => b.BookingMadeAt)
                    .IsRequired();

                entity.HasOne(b => b.TravelPackage)
                    .WithMany() // Flere Bookings kan være knyttet til ét TravelPackage
                    .HasForeignKey(b => b.TravelPackageId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                entity.OwnsOne(b => b.Price, price =>
                {
                    price.Property(p => p.Amount)
                        .HasColumnName("PriceAmount")

                        .HasPrecision(18, 0)
                        .IsRequired();

                    price.Property(p => p.Currency)
                        .HasColumnName("PriceCurrency")
                        .HasConversion<string>() // <-- Konverter enum til string
                        .IsRequired();
                })

                .HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId);

                entity.Property(p => p.Status)
                       .HasConversion<string>() // konverter enum til string
                       .HasMaxLength(50);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment", "dbo");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Status)
                        .HasConversion<string>() // konverter enum til string
                        .HasMaxLength(50);       // matcher nvarchar(50) i databasen

                // Mapper PriceAsDecimal til en separat tabel eller som en værdi-objekt
                entity.OwnsOne(p => p.Price, price =>
                {
                    price.Property(p => p.Amount)
                        .HasColumnName("PriceAmount")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    price.Property(p => p.Currency)
                        .HasColumnName("PriceCurrency")
                        .IsRequired()
                        .HasConversion<string>()
                        .HasMaxLength(3);
                });
            });

            modelBuilder.Entity<Traveller>(entity =>
            {
                entity.ToTable("Traveller"); // Tabellens navn

                entity.HasKey(t => t.Id); // Primærnøgle

                entity.Property(t => t.FirstName)
                      .IsRequired(); // Fornavn skal være angivet

                entity.Property(t => t.LastName)
                      .IsRequired(); // Efternavn skal være angivet

                entity.Property(t => t.DateOfBirth)
                      .IsRequired(); // Fødselsdato er obligatorisk

                entity.Property(t => t.Gender)
                      .HasConversion<string>() // Enum gemmes som string i databasen
                      .IsRequired(); // Køn skal være angivet

                entity.HasOne(t => t.Nationality) // En traveller har én nationalitet (navigation property)
                      .WithMany() // Ingen navigation fra Country til Traveller
                      .HasForeignKey(t => t.NationalityId) // Fremmednøgle i Traveller-tabellen
                      .IsRequired() // Nationalitet er påkrævet
                      .OnDelete(DeleteBehavior.Restrict); // Forhindrer sletning af country med referencer

                entity.Property(t => t.PassportNumber)
                      .IsRequired(); // Pasnummer er påkrævet

                entity.Property(t => t.PassportExpiry)
                      .IsRequired(); // Udløbsdato på pas er påkrævet

                entity.HasOne(t => t.PassportIssuingCountry) // En traveller har ét pasudstedelsesland (navigation property)
                      .WithMany() // Ingen navigation fra Country til Traveller
                      .HasForeignKey(t => t.PassportIssuingCountryId) // Fremmednøgle i Traveller-tabellen
                      .IsRequired() // Udstedelsesland for pas er påkrævet
                      .OnDelete(DeleteBehavior.Restrict); // Forhindrer sletning af country med referencer

                entity.HasOne(t => t.Booking) // Hver traveller tilhører én booking
                      .WithMany(b => b.travellers) // En booking kan have mange travellers
                      .HasForeignKey(t => t.BookingId) // Fremmednøgle i Traveller-tabellen
                      .IsRequired() // Hver traveller SKAL være knyttet til en booking
                      .OnDelete(DeleteBehavior.Cascade); // Hvis en booking slettes, slettes alle tilknyttede travellers
            });

        }
    }
}