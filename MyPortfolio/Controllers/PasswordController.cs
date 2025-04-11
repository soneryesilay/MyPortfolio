using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class PasswordController : Controller
    {
        [HttpGet]
        public IActionResult Change()
        {
            return View();
        }

        [HttpPost]
        // Normalde burada şifre değiştirme mantığı olurdu ama basit auth ile mümkün değil.
        public IActionResult Change(object model) // ViewModel tanımlamadık şimdilik
        {
            // Sadece bir mesaj gösterip geri yönlendirelim
            TempData["PasswordMessage"] = "Mevcut basit kimlik doğrulama sistemiyle şifre değiştirme işlemi desteklenmemektedir. Bu özellik için ASP.NET Core Identity gibi gelişmiş bir sistem gereklidir.";
            return RedirectToAction("Change");
        }
    }
}