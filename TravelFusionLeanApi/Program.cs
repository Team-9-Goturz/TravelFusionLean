using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using TravelFusionLeanApi.Services;


/// <summary>
/// Program.cs er hovedindgangspunktet for API-applikationen og opsætter alle nødvendige services, middleware og endpoints.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Tilføjer controller-understøttelse og konfigurerer JSON-serielisering, så enums bliver sendt som tekst i stedet for tal.
/// </summary>
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

/// <summary>
/// Tilføjer automatisk generering af OpenAPI-dokumentation (Swagger) for at gøre API'et lettere at teste og dokumentere.
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
/// Registrerer en HttpClient til at kalde MockFlightsAPI. BaseAddress svarer til mock-API'ets URL.
/// </summary>
builder.Services.AddHttpClient<IFlightService, FlightService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7115/"); // MockFlightsAPI kører på port 7115
});

/// <summary>
/// Registrerer en HttpClient til at kalde MockHotelsAPI. BaseAddress svarer til mock-API'ets URL.
/// </summary>
builder.Services.AddHttpClient<IHotelService, HotelService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7121/"); // MockHotelsAPI kører på port 7121
});

/// <summary>
/// Bygger applikationen baseret på de konfigurerede services.
/// </summary>
var app = builder.Build();

/// <summary>
/// Konfigurerer middleware til Swagger, som aktiveres i udviklingsmiljø.
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelFusionLean API v1");
        options.RoutePrefix = string.Empty;
    });
}

/// <summary>
/// Tvinger brug af HTTPS til sikker kommunikation.
/// </summary>
app.UseHttpsRedirection();

/// <summary>
/// Aktiverer autorisation, hvis nødvendigt i applikationen (f.eks. til sikrede endpoints).
/// </summary>
app.UseAuthorization();

/// <summary>
/// Mapper controller-ruterne, så API-endpoints bliver tilgængelige.
/// </summary>
app.MapControllers();

/// <summary>
/// Starter applikationen.
/// </summary>
app.Run();
