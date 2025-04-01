using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using TravelFusionLeanApi.Services;
using Microsoft.EntityFrameworkCore;
using Data;
using ServiceContracts;
using ServiceImplementations;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Tilføjer controller-understøttelse og konfigurerer JSON-serielisering, så enums bliver sendt som tekst i stedet for tal.
/// </summary>
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

/// <summary>
/// Konfigurerer Entity Framework Core til at bruge SQL Server baseret på connection string i appsettings.json.
/// </summary>
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/// <summary>
/// Tilføjer Swagger/OpenAPI support til at dokumentere og teste API’et.
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TravelFusionLean API",
        Version = "v1",
        Description = "API til integration af mock-fly og hoteller i rejsepakker"
    });
});

/// <summary>
/// Konfigurerer CORS så Blazor frontend (fx på https://localhost:7177) kan få adgang til API’et.
/// </summary>
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7177")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

/// <summary>
/// Registrerer en HttpClient til at kalde MockFlightsAPI. BaseAddress svarer til mock-API'ets URL.
/// </summary>
builder.Services.AddHttpClient<IFlightService, FlightService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7115/");
});

/// <summary>
/// Registrerer en HttpClient til at kalde MockHotelsAPI. BaseAddress svarer til mock-API'ets URL.
/// </summary>
builder.Services.AddHttpClient<IHotelService, HotelService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7121/");
});

/// <summary>
/// Registrerer databasebaserede services som skal bruges i dine controllers.
/// </summary>
builder.Services.AddScoped<ITravelPackageService, TravelPackageService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

var app = builder.Build();

/// <summary>
/// Aktiverer Swagger-dokumentation i udviklingsmiljø.
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelFusionLean API v1");
        options.RoutePrefix = string.Empty; // Swagger vises direkte på root (https://localhost:7274/)
    });
}

/// <summary>
/// Tvinger HTTPS på alle requests.
/// </summary>
app.UseHttpsRedirection();

/// <summary>
/// Aktiverer CORS så frontend kan tilgå API’et.
/// </summary>
app.UseCors("AllowFrontend");

/// <summary>
/// Autorisation (kan bruges til fremtidig rollebeskyttelse).
/// </summary>
app.UseAuthorization();

/// <summary>
/// Mapper controller-ruter til endpoints.
/// </summary>
app.MapControllers();

/// <summary>
/// Starter webapplikationen.
/// </summary>
app.Run();
