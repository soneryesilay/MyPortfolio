using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; // ClaimsTypes için

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            // Giriş yapmış kullanıcının adını Claim'den alalım (basit auth'da sadece username var)
            ViewBag.Username = User.FindFirstValue(ClaimTypes.Name) ?? "Bilinmiyor";
            return View();
        }
    }
}