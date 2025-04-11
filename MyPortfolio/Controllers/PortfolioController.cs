using Microsoft.AspNetCore.Authorization; // Login koruması için
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System.Linq;

namespace MyPortfolio.Controllers
{
    [Authorize] // Bu controller'a sadece giriş yapmış kullanıcılar erişebilir
    public class PortfolioController : Controller
    {
        private readonly MyPortfolioContext _context;

        public PortfolioController(MyPortfolioContext context)
        {
            _context = context;
        }

        // GET: /Portfolio/Index
        public IActionResult Index()
        {
            var values = _context.Portfolios.ToList();
            return View(values);
        }

        // GET: /Portfolio/CreatePortfolio
        [HttpGet]
        public IActionResult CreatePortfolio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePortfolio(Portfolio portfolio)
        {
            // ImageUrl null veya boş ise default değer ata
            if (string.IsNullOrWhiteSpace(portfolio.ImageUrl))
            {
                portfolio.ImageUrl = ""; 
            }
            else if(string.IsNullOrWhiteSpace(portfolio.Url))  
            {
               portfolio.Url = "";
            }

            _context.Portfolios.Add(portfolio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: /Portfolio/DeletePortfolio/5
        public IActionResult DeletePortfolio(int id)
        {
            var value = _context.Portfolios.Find(id);
            if (value != null)
            {
                _context.Portfolios.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: /Portfolio/UpdatePortfolio/5
        [HttpGet]
        public IActionResult UpdatePortfolio(int id)
        {
            var value = _context.Portfolios.Find(id);
            if (value == null)
            {
                return RedirectToAction("Index"); // Kayıt bulunamazsa listeye dön
            }
            return View(value);
        }

        // POST: /Portfolio/UpdatePortfolio
        [HttpPost]
        public IActionResult UpdatePortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}