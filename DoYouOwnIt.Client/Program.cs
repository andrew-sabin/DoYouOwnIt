using DoYouOwnIt.Client;
using DoYouOwnIt.Client.Services;
using DoYouOwnIt.Shared.Entities;
using DoYouOwnIt.Shared.Models.Product;
using Mapster;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Blazored.Typeahead;
using Sqids;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

int minLength = builder.Configuration.GetValue<int>("Sqids:MinLength");
string alphabet = builder.Configuration["Sqids:Alphabet"];


builder.Services.AddSingleton(sp =>
    new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build()
   );

builder.Services.AddScoped(typeof(Blazored.Typeahead.BlazoredTypeahead<,>));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IFormatService, FormatService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddSingleton(new SqidsEncoder<int>(new()
{
    MinLength = minLength,
    Alphabet = alphabet,
}));

builder.Services.AddSyncfusionBlazor();
await builder.Build().RunAsync();
