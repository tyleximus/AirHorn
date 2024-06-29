using AirHorn.App;
using AirHorn.App.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

var apiUrl = builder.Configuration["apiUrl"] ?? builder.HostEnvironment.BaseAddress;

builder.Services
    .AddScoped<IHttpService, HttpService>()
    .AddScoped<ILocalStorageService, LocalStorageService>()
    .AddScoped<IAirHornService, AirHornService>()
    //.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

builder.Services.AddTelerikBlazor();

await builder.Build().RunAsync();
