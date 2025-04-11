// Program.cs (Minimal API'ler veya .NET 6+ �ablonu i�in)

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context; // Bu using eklendi

var builder = WebApplication.CreateBuilder(args);
// Program.cs (ASP.NET Core 6.0 ve �zeri i�in)

builder.Services.AddDbContext<MyPortfolioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- Kimlik Do�rulama Servislerini Ekle ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MyPortfolio.AuthCookie"; // �erez ad� (iste�e ba�l�)
        options.LoginPath = "/Login/Index"; // Giri� yap�lmam��sa y�nlendirilecek sayfa
        options.LogoutPath = "/Login/Logout"; // ��k�� i�lemi i�in yol (iste�e ba�l�)
        options.AccessDeniedPath = "/Login/AccessDenied"; // Yetkisiz eri�im denemesinde y�nlendirilecek sayfa (iste�e ba�l�)
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Oturum s�resi
        options.SlidingExpiration = true; // Aktivite olduk�a s�reyi uzat
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

// --- Kimlik Do�rulama ve Yetkilendirme Middleware'lerini Ekle ---
// �NEML�: UseRouting'den sonra, UseEndpoints/MapControllerRoute'dan �nce olmal�!
app.UseAuthentication(); // Kimlik Do�rulama
app.UseAuthorization();  // Yetkilendirme
// --- Bitti ---


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}"); // Varsay�lan rotan�z farkl�ysa ona g�re ayarlay�n

app.Run();