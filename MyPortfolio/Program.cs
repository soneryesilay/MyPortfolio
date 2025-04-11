// Program.cs (Minimal API'ler veya .NET 6+ þablonu için)

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context; // Bu using eklendi

var builder = WebApplication.CreateBuilder(args);
// Program.cs (ASP.NET Core 6.0 ve üzeri için)

builder.Services.AddDbContext<MyPortfolioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- Kimlik Doðrulama Servislerini Ekle ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MyPortfolio.AuthCookie"; // Çerez adý (isteðe baðlý)
        options.LoginPath = "/Login/Index"; // Giriþ yapýlmamýþsa yönlendirilecek sayfa
        options.LogoutPath = "/Login/Logout"; // Çýkýþ iþlemi için yol (isteðe baðlý)
        options.AccessDeniedPath = "/Login/AccessDenied"; // Yetkisiz eriþim denemesinde yönlendirilecek sayfa (isteðe baðlý)
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Oturum süresi
        options.SlidingExpiration = true; // Aktivite oldukça süreyi uzat
    });
// --- Bitti ---


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// --- Kimlik Doðrulama ve Yetkilendirme Middleware'lerini Ekle ---
// ÖNEMLÝ: UseRouting'den sonra, UseEndpoints/MapControllerRoute'dan önce olmalý!
app.UseAuthentication(); // Kimlik Doðrulama
app.UseAuthorization();  // Yetkilendirme
// --- Bitti ---


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}"); // Varsayýlan rotanýz farklýysa ona göre ayarlayýn

app.Run();