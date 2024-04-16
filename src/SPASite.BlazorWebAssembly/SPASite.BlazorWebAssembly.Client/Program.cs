using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SPASite.BlazorWebAssembly.Client.Domain;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddClientDomain();
await builder.Build().RunAsync();
