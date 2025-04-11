using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context; // Context için using eklendi

namespace MyPortfolio.ViewComponents // Namespace'i kontrol et
{
    public class _TestimonialComponentPartial : ViewComponent
    {
        // Veritabanı bağlantısı için Context nesnesi
        private readonly MyPortfolioContext _context;

        // Constructor Injection ile Context'i alalım
        public _TestimonialComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Veritabanından tüm referansları çek
            var values = _context.Testimonials.ToList();
            // Verileri View'a model olarak gönder
            return View(values);
        }
    }
}