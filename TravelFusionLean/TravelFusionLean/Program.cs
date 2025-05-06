using Configuration;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceImplementations;
using Shared.Models;
using TravelFusionLean.Components;
using Data;
using TravelFusionLean;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Her henter vi connection string fra appsettings.json
var configuration = builder.Configuration;

// Tilføj DbContext med migrations assembly sat korrekt
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.MigrationsAssembly("Data"))); 

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

builder.Services.AddScoped<ProtectedSessionStorage>();

builder.Services.AddHttpClient("AuthApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7274"); // ← use this
});

builder.Services.AddAuthorizationCore();
builder.Services.Configure<StripeSettings>(configuration.GetSection("Stripe"));


try
{
    var app = builder.Build();

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
