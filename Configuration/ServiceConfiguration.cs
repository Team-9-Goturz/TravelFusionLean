using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceContracts;
using ServiceImplementations;


namespace Configuration
{
    /// <summary>
    /// Indeholder en extension-metode til konfiguration af services og afhængigheder (Dependency Injection).
    /// Kaldes fra Program.cs i hovedprojektet.
    /// </summary>
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
            }

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<ITravelPackageService, TravelPackageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICountryReadService, CountryReadService>();
            services.AddScoped<IBookingCoordinator, BookingCoordinator>();
            services.AddScoped<ICurrencyConverter, CurrencyConverter>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IHotelStayService, HotelStayService>();

        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null or empty.");
            }

            services.ConfigureServices(connectionString);
        }
    }

}
