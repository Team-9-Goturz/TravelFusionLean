using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using TravelFusionLeanApi.Services;

var builder = WebApplication.CreateBuilder(args);

/// Tilføjer controller-understøttelse og enum-serialisering
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
        policy.WithOrigins("https://localhost:7177")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

/// Registrerer HttpClients til mock-API’er
builder.Services.AddHttpClient<IFlightService, FlightService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7115/");
});

builder.Services.AddHttpClient<IHotelService, HotelService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7121/");
});

var app = builder.Build();

/// Swagger vises kun i udviklingsmiljø
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelFusionLean API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();
