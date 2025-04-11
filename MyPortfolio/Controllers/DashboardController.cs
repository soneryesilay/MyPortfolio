using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities; // Entities için eklendi
using System.Linq;
using System.Security.Claims; // ClaimsTypes için

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly MyPortfolioContext _context;

        public DashboardController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Giriş yapan kullanıcı adı
            ViewBag.Username = User.FindFirstValue(ClaimTypes.Name) ?? "Admin";

            // Son eklenen 3 projeyi al (ID'ye göre ters sırala)
            ViewBag.LatestPortfolios = _context.Portfolios
                                            .OrderByDescending(p => p.PortfolioId)
                                            .Take(3)
                                            .ToList();

            // Son eklenen 3 deneyimi al
            ViewBag.LatestExperiences = _context.Experiences
                                             .OrderByDescending(e => e.ExperienceId)
                                             .Take(3)
                                             .ToList();

            // İsteğe bağlı: Birkaç temel istatistik (farklı sunum için)
            ViewBag.TotalSkills = _context.Skills.Count();
            ViewBag.TotalMessages = 0; // Eğer bir mesajlaşma sistemi varsa eklenebilir, şimdilik 0

            return View();
        }
    }
}