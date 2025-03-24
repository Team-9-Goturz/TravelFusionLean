using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TravelFusionLean.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app"); // <- Hvis ikke allerede

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7274") // Din API-port
});

await builder.Build().RunAsync();
;