using Configuration;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceImplementations;
using Shared.Models;
using TravelFusionLean.Components;
using Data;
using TravelFusionLean;


var builder = WebApplication.CreateBuilder(args);

// Her henter vi connection string fra appsettings.json
var configuration = builder.Configuration;

// 1. Tilføj Stripe-konfiguration fra appsettings.json
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
// 2. Tilføj StripeService til Dependency Injection
builder.Services.AddSingleton<StripeService>();

// CORS-konfiguration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowStripe", policy =>
    {
        policy.WithOrigins("https://dd13-192-38-145-135.ngrok-free.app","https://localhost:7177")  // Ngrok URL
                          
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Tilføj DbContext med migrations assembly sat korrekt
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.MigrationsAssembly("Data"))); // 👈 Her specificeres migrations-projektet

/// <summary>
/// Tilføjer Razor-komponenter (Blazor) med understøttelse for WebAssembly og Server-rendering.
/// </summary>
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.ConfigureServices(configuration);
builder.Services.AddScoped<TravelPackageState>();


builder.Services.AddHttpClient<IFlightApiService, FlightApiService>();
builder.Services.AddHttpClient<IHotelApiService, HotelApiService>();


try
{

    var app = builder.Build();

    // Brug CORS politikken for at tillade de nødvendige domæner
    app.UseCors("AllowStripe");

    /// <summary>
    /// Konfigurerer middleware til udvikling og produktion.
    /// </summary>
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseAntiforgery();

    /// <summary>
    /// Konfigurerer routing og rendering for Blazor-komponenter.
    /// </summary>
    app.MapStaticAssets();
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode();



    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("Fatal fejl under opstart: " + ex.Message);
    Console.WriteLine("Stacktrace: " + ex.StackTrace);
    throw;
}
