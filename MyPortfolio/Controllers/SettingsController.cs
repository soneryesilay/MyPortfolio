using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System.Linq;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly MyPortfolioContext _context;

        public SettingsController(MyPortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Genellikle tek bir ayar satırı olur, onu bulalım veya yoksa yeni oluşturalım
            var settings = _context.SiteSettings.FirstOrDefault();
            if (settings == null)
            {
                settings = new SiteSettings(); // Boş bir nesne oluştur
            }
            return View(settings);
        }

        [HttpPost]
        public IActionResult Index(SiteSettings settings)
        {
            if (ModelState.IsValid)
            {
                var existingSettings = _context.SiteSettings.Find(settings.SiteSettingsId);
                if (existingSettings != null)
                {
                    // Var olanı güncelle
                    _context.Entry(existingSettings).CurrentValues.SetValues(settings);
                }
                else
                {
                    // Yoksa yeni ekle (ID'si 0 ise veya veritabanında bulunamadıysa)
                    // Eğer ID her zaman 1 olacaksa:
                    settings.SiteSettingsId = 1; // ID'yi 1 olarak ayarla
                    var settingsWithId1 = _context.SiteSettings.Find(1);
                    if (settingsWithId1 != null)
                    {
                        _context.Entry(settingsWithId1).CurrentValues.SetValues(settings);
                    }
                    else
                    {
                        _context.SiteSettings.Add(settings);
                    }
                    // context.SiteSettings.Add(settings); // ID otomatik artan ise bu kullanılır
                }
                _context.SaveChanges();
                TempData["SettingsMessage"] = "Ayarlar başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            // Model geçerli değilse formu tekrar göster
            return View(settings);
        }
    }
}