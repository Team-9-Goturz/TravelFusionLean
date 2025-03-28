using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceContracts;
using ServiceImplementations;

namespace Configuration
{
    /// <summary>
    /// Indeholder metode til at konfigurere og tilføje services til DI-containeren (Dependency Injection),
    /// herunder databasekontekst og servicelag.
    /// </summary>
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Tilføjer nødvendige services til DI-containeren (DbContext, UserService og UserRoleService).
        /// </summary>
        public static void  ConfigureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=85.10.209.22;Database=TravelFusion;User ID=team09;Password=GdrnJu64D3Lo!;TrustServerCertificate=True;"));

            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IUserRoleService, UserRoleService>();
        }
    }
}
