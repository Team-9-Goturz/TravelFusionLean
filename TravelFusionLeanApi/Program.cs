using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using TravelFusionLeanApi.Services;
using Microsoft.Extensions.Options;
using TravelFusionLeanApi.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ServiceImplementations;
using Shared.Configuration;
using ServiceContracts;


var builder = WebApplication.CreateBuilder(args);


/// <summary>
/// Binder konfigurationsafsnittet 'ApiSettings' fra appsettings.json til en stærkt typet klasse, så det kan bruges i hele applikationen.
/// </summary>
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

/// Tilf�jer controller-underst�ttelse og enum-serialisering
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

/// Swagger/OpenAPI support
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

/// CORS konfiguration (tillader Blazor frontend adgang)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://travelfusionapp-aqbfg6e2bhenb8e3.canadacentral-01.azurewebsites.net")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

/// <summary>
/// Registrerer HttpClient til FlightService. Bruger base URL fra ApiSettings-konfigurationen.
/// </summary>
builder.Services.AddHttpClient<IFlightService, FlightService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(settings.MockFlightsApiUrl);
});

/// <summary>
/// Registrerer HttpClient til HotelService. Bruger kun base URL hvis den er sat i ApiSettings.
/// Dette gør det muligt at undgå fejl, hvis MockHotelsAPI ikke er deployet.
/// </summary>
builder.Services.AddHttpClient<IHotelService, HotelService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    if (!string.IsNullOrWhiteSpace(settings.MockHotelsApiUrl))
    {
        client.BaseAddress = new Uri(settings.MockHotelsApiUrl);
    }
});

// Henter konfiguration fra appsettings (f.eks. JWT)
var config = builder.Configuration;

// 2. Konfigurer JWT Bearer-godkendelse
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // hemmelig nøgle fra konfiguration
        var secret = config["JwtSettings:Secret"];
        
        // Angiver regler for validering af token
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Tjek at token kommer fra en godkendt udsteder
            ValidateAudience = true, // Tjek at token er beregnet til denne API
            ValidateLifetime = true, // Tjek at token ikke er udløbet
            ValidateIssuerSigningKey = true, // Tjek at token-signaturen er gyldig
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
        };
    });

// kræves for at bruge [Authorize]-attributter
builder.Services.AddAuthorization();

var app = builder.Build();




app.UseAuthentication();



/// <summary>
/// Aktiverer Swagger UI i både Development og Production, med korrekt endpoint afhængigt af miljø.
/// </summary>
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var swaggerUrl = "/swagger/v1/swagger.json";
        options.SwaggerEndpoint(swaggerUrl, "TravelFusionLean API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "TravelFusionLean API is running!");
app.Run();
