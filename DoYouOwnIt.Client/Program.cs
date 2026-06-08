using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Captcha.ReCaptcha;
using EasyAppDev.Blazor.AutoComplete.Extensions;
using DoYouOwnIt.Client;
using DoYouOwnIt.Client.Services;
using DoYouOwnIt.Client.Services.Interface;
using GoogleCaptchaComponent;
using GoogleCaptchaComponent.Configuration;
using GoogleCaptchaComponent.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sqids;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HTTP client for API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFormatService, FormatService>();
builder.Services.AddScoped<IFormatTypeService, FormatTypeService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INewsBlogService, NewsBlogService>();
builder.Services.AddScoped<IFormatRevisionService, FormatRevisionService>();
builder.Services.AddScoped<IUserTimeZoneService, UserTimeZoneService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddSyncfusionBlazor();

builder.Services.AddGoogleCaptcha(configuration => {
    configuration.V2SiteKey = "Removed";
    configuration.DefaultVersion = CaptchaConfiguration.Version.V2;
    configuration.DefaultTheme = CaptchaConfiguration.Theme.Light;
    configuration.DefaultLanguage = CaptchaLanguages.English;
});

builder.Services.AddAutoComplete();

await builder.Build().RunAsync();
builder.Services.AddTransient<AuthMessageHandler>();
builder.Services.AddHttpClient<AuthService>()
    .AddHttpMessageHandler<AuthMessageHandler>();
