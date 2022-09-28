using BooksSpotLibrary.Areas.Identity.Authorization;
using BooksSpotLibrary.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

});

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;

//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters =
//    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;
//});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    // Cookie settings
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

//    options.LoginPath = "/Identity/Account/Login";
//    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//    options.SlidingExpiration = true;
//});

builder.Services.AddDbContext<BooksSpotLibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BooksSpotLibraryContext") ?? throw new InvalidOperationException("Connection string 'BooksSpotLibraryContext' not found.")));

// Authorization handlers.
builder.Services.AddScoped<IAuthorizationHandler,
                      UserIsOwnerAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler,
                      LibrarianAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler,
                      UserAuthorizationHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) //seeding the users
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    // requires using Microsoft.Extensions.Configuration;
    // Set password with the Secret Manager tool.
    //dotnet user-secrets set SeedLibrarianPW1 "123Abc?"
    //dotnet user-secrets set SeedLibrarianPW2 "456Def?"
    //dotnet user-secrets set SeedUserPW1 "789Ghi?"
    //dotnet user-secrets set SeedUserPW2 "123Jkl?"


    var testLibrarianPw1 = builder.Configuration.GetValue<string>("SeedLibrarianPW1");
    var testLibrarianPw2 = builder.Configuration.GetValue<string>("SeedLibrarianPW2");
    var testUserPw1 = builder.Configuration.GetValue<string>("SeedUserPW1");
    var testUserPw2 = builder.Configuration.GetValue<string>("SeedUserPW2");

    await SeedData.Initialize(services, testLibrarianPw1, testLibrarianPw2, testUserPw1, testUserPw2);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
