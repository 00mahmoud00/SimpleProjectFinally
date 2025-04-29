using Microsoft.EntityFrameworkCore;
using SimpleLibrary;
using SimpleLibrary.Services.ExceptionsLog;
using SimpleLibrary.Services.IO;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IExceptionLogService, ExceptionLogService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddDbContext<SimpleLibraryDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder
    .Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddUserManager<UserManager<IdentityUser>>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<SimpleLibraryDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
var app = builder.Build();

{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.Use(async (context, next) =>
{
    if (context.User?.Identity?.IsAuthenticated == true)
    {
        Console.WriteLine("==== User Claims ====");
        foreach (var claim in context.User.Claims)
        {
            Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
        }
        Console.WriteLine("======================");
    }
    else
        Console.WriteLine("User is not authenticated.");

    await next();
});
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
// ReSharper disable once RedundantSuppressNullableWarningExpression
await ApplicationDbInitializer.SeedUsersAndRolesAsync(builder.Services.BuildServiceProvider()!);
app.Run();