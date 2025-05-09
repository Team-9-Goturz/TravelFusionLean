using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceContracts;
using ServiceImplementations;
using Shared.Configuration;
using System.Text;
using System.Text.Json.Serialization;
using TravelFusionLeanApi.Configuration;
using TravelFusionLeanApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrer services
builder.Services.AddScoped<StripeService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Binder konfiguration til stærkt typede klasser
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

// Tilføj controller-understøttelse og enum-serialisering
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Swagger/OpenAPI support
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

// Konfigurer CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "https://travelfusionapp-aqbfg6e2bhenb8e3.canadacentral-01.azurewebsites.net",
                "https://localhost:7177",
                "http://localhost:5096"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Registrer HttpClient til FlightModelService
builder.Services.AddHttpClient<IFlightService, TravelFusionLeanApi.Services.FlightService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(settings.MockFlightsApiUrl);
});

// Registrer HttpClient til HotelService
builder.Services.AddHttpClient<IHotelService, HotelService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    if (!string.IsNullOrWhiteSpace(settings.MockHotelsApiUrl))
    {
        client.BaseAddress = new Uri(settings.MockHotelsApiUrl);
    }
});

// Konfigurer JWT Bearer-godkendelse
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secret = builder.Configuration["JwtSettings:Secret"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
        };
    });

// Tilføj autorisation
builder.Services.AddAuthorization();

// Konfigurer databaseforbindelse
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Configuration.ServiceConfiguration.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Aktiver Swagger UI i både Development og Production
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelFusionLean API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "TravelFusionLean API is running!");

app.Run();
