using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using System.Linq;

namespace MyPortfolio.ViewComponents // Namespace'i kontrol et (MyPortolio değil MyPortfolio olmalı)
{
    public class _ContactComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext _context;

        // Constructor Injection
        public _ContactComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // İlk veya tek iletişim bilgisini çek
            var contactInfo = _context.Contacts.FirstOrDefault();
            // Veriyi View'a model olarak gönder
            return View(contactInfo);
        }
    }
}