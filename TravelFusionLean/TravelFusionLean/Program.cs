using Configuration;
using Microsoft.EntityFrameworkCore;
using TravelFusionLean.Components;


var builder = WebApplication.CreateBuilder(args);

// Her henter vi connection string fra appsettings.json
var configuration = builder.Configuration;


/// <summary>
/// Tilføjer Razor-komponenter (Blazor) med understøttelse for WebAssembly og Server-rendering.
/// </summary>
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Tilføjer services og databasekonfiguration vha. vores fælles extension-metode
builder.Services.ConfigureServices(configuration);

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
