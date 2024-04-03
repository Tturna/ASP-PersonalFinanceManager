using Microsoft.EntityFrameworkCore;
using PersonalFinances.Models;
using PersonalFinances.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("MySql") ??
                       "Server=localhost;Database=finance;User=root;Password=root;";

var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

builder.Services.AddAuthentication().AddCookie(cookieAuthOptions =>
{
    cookieAuthOptions.LoginPath = "/login";
    cookieAuthOptions.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    cookieAuthOptions.SlidingExpiration = true;
    cookieAuthOptions.Cookie.Name = "PersonalFinancesAuth";
    cookieAuthOptions.Cookie.HttpOnly = true;
    cookieAuthOptions.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<HashingService>();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();