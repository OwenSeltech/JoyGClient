using JoyGClient.Data;
using JoyGClient.Entities;
using JoyGClient.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager _config = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddApplicationServices(_config);
builder.Services.AddIdentityServices(_config);



// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<Roles>>();
    await context.Database.MigrateAsync();
    await Seed.SeedRoles(roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
