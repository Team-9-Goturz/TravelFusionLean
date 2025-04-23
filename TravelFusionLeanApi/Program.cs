using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using TravelFusionLeanApi.Services;

var builder = WebApplication.CreateBuilder(args);

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

/// Registrerer HttpClients til mock-API�er
builder.Services.AddHttpClient<IFlightService, FlightService>(client =>
{
    client.BaseAddress = new Uri("https://mockflightapi-webapp.azurewebsites.net");
});

//mockhotelsapi stadig ikke deployed
builder.Services.AddHttpClient<IHotelService, HotelService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5144/");
});

var app = builder.Build();

/// Swagger vises
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelFusionLean API v1");
    options.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();
