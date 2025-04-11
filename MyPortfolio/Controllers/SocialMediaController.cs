using Microsoft.AspNetCore.Authorization; // Login koruması için
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System.Linq;

namespace MyPortfolio.Controllers
{
    [Authorize] // Bu controller'a sadece giriş yapmış kullanıcılar erişebilir
    public class SocialMediaController : Controller
    {
        private readonly MyPortfolioContext _context;

        public SocialMediaController(MyPortfolioContext context)
        {
            _context = context;
        }

        // GET: /SocialMedia/Index
        public IActionResult Index()
        {
            var values = _context.SocialMedias.ToList();
            return View(values);
        }

        // GET: /SocialMedia/CreateSocialMedia
        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        // POST: /SocialMedia/CreateSocialMedia
        [HttpPost]
        public IActionResult CreateSocialMedia(SocialMedia socialMedia)
        {
            _context.SocialMedias.Add(socialMedia);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /SocialMedia/DeleteSocialMedia/5
        public IActionResult DeleteSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value != null)
            {
                _context.SocialMedias.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: /SocialMedia/UpdateSocialMedia/5
        [HttpGet]
        public IActionResult UpdateSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value == null)
            {
                return RedirectToAction("Index"); // Kayıt bulunamazsa listeye dön
            }
            return View(value);
        }

        // POST: /SocialMedia/UpdateSocialMedia
        [HttpPost]
        public IActionResult UpdateSocialMedia(SocialMedia socialMedia)
        {
            _context.SocialMedias.Update(socialMedia);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}