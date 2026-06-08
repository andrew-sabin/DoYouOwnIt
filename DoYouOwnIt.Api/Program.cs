using DoYouOwnIt.Api.Middleware;
using DoYouOwnIt.Api.Scripts.Seeds;
using DoYouOwnIt.Shared.Configuration;
using DoYouOwnIt.Shared.Models.Account;
using DoYouOwnIt.Shared.Models.User;
using DoYouOwnIt_Shared.Entities;
using DoYouOwnIt_Shared.Models.NewsUpdate;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using DoYouOwnIt.Api.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add controllers. OData will be configured after the EDM model is built below.
// Note: we configure AddOData later so it can receive the built EDM model.
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
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedAccount = true; // Set to true for production

    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
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

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                ctx.Request.Cookies.TryGetValue("token", out var accessToken);
                if (!string.IsNullOrEmpty(accessToken))
                    ctx.Token = accessToken;

                return Task.CompletedTask;
            }
        };
    });

// Configure SMTP2GO
builder.Services.Configure<Smtp2GoSettings>(
    builder.Configuration.GetSection("Smtp2GoSettings"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFormatRepository, FormatRepository>();
builder.Services.AddScoped<IFormatService, Format_Service>();
builder.Services.AddScoped<IFormatTypeRepository, FormatTypeRepository>();
builder.Services.AddScoped<IFormatTypeService, FormatTypeService>();
builder.Services.AddScoped<IFormatRevisionRepository, FormatRevisionRepository>();
builder.Services.AddScoped<IFormatRevisionService, FormatRevisionService>();

builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INewsBlogRepository, NewsBlogRepository> ();
builder.Services.AddScoped<INewsBlogService, NewsBlogService>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ModeratorsOnly", policy => policy.RequireRole("Moderator"));
    options.AddPolicy("AlphaUsersOnly", policy => policy.RequireRole("AlphaUser"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

var modelBuilder = new ODataConventionModelBuilder();
// Use singular entity set name to match the OData controller name `ProductController` so
// the endpoint will be reachable at `odata/Product`.
modelBuilder.EntitySet<Product>("Product");

var edmModel = modelBuilder.GetEdmModel();

// Register controllers and OData with the built EDM model so routes like
// 'odata/Product' are available.
builder.Services.AddControllers().AddOData(options =>
    options.AddRouteComponents("odata", edmModel)
           .Select()
           .Filter()
           .OrderBy()
           .Expand()
           .Count()
           .SetMaxTop(10));

builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddBlobServiceClient(builder.Configuration["storageConnection"]);
});

var app = builder.Build();

TypeAdapterConfig<Availability, AvailabilityResponse>
    .NewConfig()
    .Map(dest => dest.URL, src => src.URL);

TypeAdapterConfig<Category, CategoryResponse>
    .NewConfig()
    .Map(dest => dest.lockedReason, src => src.lockedReason)
    .Map(dest => dest.CreatorsTitle, src => src.CreatorsTitle)
    .Map(dest => dest.FormatsTitle, src => src.FormatsTitle)
    .Map(dest => dest.TypeTitle, src => src.TypeTitle)
    .Map(dest => dest.EditionTitle, src => src.EditionTitle);

TypeAdapterConfig<Product, ProductResponse>
    .NewConfig()
    .Map(dest => dest.lockedReason, src => src.lockedReason);

TypeAdapterConfig<Format, FormatResponse>
    .NewConfig()
    .Map(dest => dest.Product, src => src.Product);

TypeAdapterConfig<FormatType, FormatTypeResponse>
    .NewConfig()
    .Map(dest => dest.ImageUrl, src => src.ImageUrl);

TypeAdapterConfig<ApplicationUser, UserResponse>
    .NewConfig()
    .Map(dest => dest.DateOfBirth, src => src.DateOfBirth);

TypeAdapterConfig<NewsBlog, NewsBlogResponse>
    .NewConfig()
    .Map(dest => dest.CoverImageUrl, src => src.CoverImageUrl)
    .Map(dest => dest.StickToFrontPage, src => src.StickToFrontPage);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var logger = services.GetRequiredService<ILogger<ApplicationDbContextSeed>>();

        await ApplicationDbContextSeed.SeedAsync(context, userManager, roleManager, logger);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured while seeding the database");
    }
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
app.MapControllers(); // for API Calls
app.MapFallbackToFile("index.html");

app.Run();
