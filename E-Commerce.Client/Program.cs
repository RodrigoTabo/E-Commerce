using E_Commerce.Client;
using E_Commerce.Client.ApiRoutes;
using E_Commerce.Client.Common;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();


//Servicios
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<TokenStorageService>();

builder.Services.AddScoped<AuthHandler>();

builder.Services.AddScoped<JwtAuthStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthStateProvider>());

builder.Services.AddScoped<ApiHttpClientProvider>();

builder.Services.AddScoped<AuthHandler>();

builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7145/");
})
.AddHttpMessageHandler<AuthHandler>();

builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("Api");
});

builder.Services.AddScoped<AuthApi>();
builder.Services.AddScoped<ProductoApi>();
builder.Services.AddScoped<CarritoApi>();
builder.Services.AddScoped<MetodoEnvioApi>();
builder.Services.AddScoped<DomicilioApi>();
builder.Services.AddScoped<OrdenApi>();
builder.Services.AddScoped<UserApi>();

await builder.Build().RunAsync();
