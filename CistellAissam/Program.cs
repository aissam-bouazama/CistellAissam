using CistellAissam.Data;
using CistellAissam.Models;
using CistellAissam.Repository;
using CistellAissam.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

ServerVersion version = MySqlServerVersion.AutoDetect(connectionString);
builder.Services.AddDbContext<TiendaContext>(options =>
    options.UseMySql(connectionString, version));
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddRazorPages();

builder.Services.AddScoped<ICistellRepository, CistellRepository>();
builder.Services.AddScoped<IProducteRepository, ProducteRepository>();
builder.Services.AddScoped<IUsuariRepository, UsuariRepository>();
//builder.Services.AddTransient<IUsuariRepository, UsuariRepositoryBD>();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TiendaContext>();
 //   dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

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
app.UseSession();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cistell}/{action=Index}/{id?}");

app.Run();
