using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context; // Context için using
using System.Linq;             // Count() için using

namespace MyPortfolio.ViewComponents // Namespace'i kontrol et (MyPortolio değil MyPortfolio olmalı)
{
    public class _StatisticComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext _context;

        // Constructor Injection
        public _StatisticComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // İstatistikleri ViewBag ile View'a gönderelim
            ViewBag.ProjectCount = _context.Portfolios.Count();
            ViewBag.TestimonialCount = _context.Testimonials.Count(); // Happy Clients için referans sayısını kullanabiliriz
            ViewBag.SkillCount = _context.Skills.Count(); // Örnek: Yetenek sayısını da ekleyelim
            ViewBag.ExperienceCount = _context.Experiences.Count(); // Örnek: Deneyim sayısını ekleyelim

            // Diğerleri şimdilik statik veya başka bir yerden alınabilir
            // ViewBag.Awards = 150;
            // ViewBag.CoffeeCups = 1256;

            return View();
        }
    }
}