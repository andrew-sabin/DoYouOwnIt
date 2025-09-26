using DoYouOwnIt.Api.Middleware;
using DoYouOwnIt.Shared.Models.Account;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add support for Blazor WebAssembly files
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { 
    c.UseInlineDefinitionsForEnums();
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDbConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to true for production
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtIssuer"],
            ValidAudience = builder.Configuration["JwtAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]!))
        };
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFormatRepository, FormatRepository>();
builder.Services.AddScoped<IFormatService, Format_Service>();
builder.Services.AddScoped<IStoreRepository,  StoreRepository>();
builder.Services.AddScoped<IStoreService,  StoreService>();
builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});
builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// TypeAdapterConfig
TypeAdapterConfig<ProductCategory, ProductCategoryResponse>
    .NewConfig()
    .Map(dest => dest.Slug, src => src.Slug);

TypeAdapterConfig<Product, ProductResponse>
    .NewConfig()
    .Map(dest => dest.Slug, src => src.Slug)
    .Map(dest => dest.CoverImageURL, src => src.CoverImageURL)
    .Map(dest => dest.IsLocked, src => src.IsLocked)
    .Map(dest => dest.Creators, src => src.Creators)
    .Map(dest => dest.CreditsURL, src => src.CreditsURL)
    .Map(dest => dest.ContentRating, src => src.ContentRating)
    .Map(dest => dest.IsAIAssisted, src => src.IsAIAssisted)
    .Map(dest => dest.AIAssistsWith, src => src.AIAssistsWith)
    .Map(dest => dest.ForMatureAudiences, src => src.ForMatureAudiences)
    .Map(dest => dest.MatureAudienceReason, src => src.MatureAudienceReason);

TypeAdapterConfig<Format, FormatResponse>
    .NewConfig()
    .Map(dest => dest.CoverImageUrl, src => src.CoverImageUrl)
    .Map(dest => dest.Slug, src => src.Slug)
    .Map(dest => dest.IsAIAssisted, src => src.IsAIAssisted)
    .Map(dest => dest.AIAssistsWith, src => src.AIAssistsWith);

TypeAdapterConfig<Store, StoreResponse>
    .NewConfig()
    .Map(dest => dest.Slug, src => src.Slug)
    .Map(dest => dest.LogoURL, src => src.LogoURL)
    .Map(dest => dest.WebsiteURL, src => src.WebsiteURL);

TypeAdapterConfig<Availability, AvailabilityResponse> 
    .NewConfig()
    .Map(dest => dest.UnitSold, src => src.UnitSold)
    .Map(dest => dest.URL, src => src.URL);

app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware to handle exceptions globally
app.UseMiddleware<ErrorHanderMiddleware>();

app.UseHttpsRedirection();

// Add support for serving Blazor WebAssembly files
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
