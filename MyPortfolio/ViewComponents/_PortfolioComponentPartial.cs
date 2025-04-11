using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context; // Context için using
using System.Linq; // ToList ve OrderBy için using

namespace MyPortfolio.ViewComponents // Namespace'i kontrol et
{
    public class _PortfolioComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext _context;

        // Constructor Injection ile Context'i al
        public _PortfolioComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Veritabanından tüm portfolyo öğelerini çek
            // İsteğe bağlı: En yeniden eskiye sıralayabilirsiniz
            var values = _context.Portfolios.OrderByDescending(p => p.PortfolioId).ToList();
            return View(values); // Verileri View'a model olarak gönder
        }
    }
}