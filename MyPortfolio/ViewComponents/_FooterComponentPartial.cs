using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities; // SocialMedia için using eklendi
using System.Linq; // ToList için using eklendi

namespace MyPortfolio.ViewComponents // Namespace'i kontrol et (MyPortolio değil MyPortfolio olmalı)
{
    public class _FooterComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext _context;

        // Constructor Injection
        public _FooterComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Veritabanından tüm sosyal medya linklerini çek
            var socialMedias = _context.SocialMedias.ToList();

            // Listeyi doğrudan View'a model olarak gönder
            return View(socialMedias);
        }
    }
}