using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.ViewModels; // LoginViewModel için using eklendi
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyPortfolio.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Login/Index
        [HttpGet]
        public IActionResult Index(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Login/Index
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF saldırılarına karşı koruma
        public async Task<IActionResult> Index(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // --- Basit Kullanıcı Adı/Şifre Kontrolü ---
                // DİKKAT: Bu kısım GÜVENLİ DEĞİLDİR ve sadece gösterim amaçlıdır!
                // Gerçek bir uygulamada ASP.NET Core Identity veya güvenli bir veritabanı kontrolü kullanın.
                if (model.Username.ToLower() == "admin" && model.Password == "12345") // Kullanıcı adı ve şifreyi burada belirleyin
                {
                    // Kullanıcı bilgileri doğruysa kimlik bilgilerini (claims) oluştur
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        // İsterseniz başka claim'ler ekleyebilirsiniz (örn: Rol)
                        // new Claim(ClaimTypes.Role, "Admin"),
                    };

                    // ClaimsIdentity ve ClaimsPrincipal oluştur
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        // Oturumun kalıcı olup olmayacağı (tarayıcı kapatılınca silinsin mi?)
                        // IsPersistent = true,

                        // Yönlendirme yapılacak URL
                        RedirectUri = returnUrl ?? Url.Action("Index", "About") // Giriş sonrası varsayılan yönlendirme (Örn: About sayfası)
                    };

                    // Kullanıcıyı sisteme giriş yap (Cookie oluşturulur)
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Belirtilen returnUrl'e veya varsayılan sayfaya yönlendir
                    // return LocalRedirect(returnUrl ?? Url.Action("Index", "About")); // Bu satır authProperties'deki RedirectUri ile aynı işi yapar
                    return Redirect(authProperties.RedirectUri); // SignInAsync içindeki yönlendirme çalışmazsa bunu kullanın
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
                    return View(model);
                }
                // --- Kontrol Bitti ---
            }

            // Model geçerli değilse formu tekrar göster
            return View(model);
        }

        // GET: /Login/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Kullanıcıyı sistemden çıkar (Cookie silinir)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login"); // Login sayfasına yönlendir
        }

        // GET: /Login/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View(); // Yetkisiz erişim sayfasını göster
        }
    }
}