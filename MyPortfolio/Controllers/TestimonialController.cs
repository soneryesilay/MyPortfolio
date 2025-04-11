using Microsoft.AspNetCore.Authorization; // Login koruması için
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System.Linq;

namespace MyPortfolio.Controllers
{
    [Authorize] // Bu controller'a sadece giriş yapmış kullanıcılar erişebilir
    public class TestimonialController : Controller
    {
        private readonly MyPortfolioContext _context;

        public TestimonialController(MyPortfolioContext context)
        {
            _context = context;
        }

        // GET: /Testimonial/Index
        public IActionResult Index()
        {
            var values = _context.Testimonials.ToList();
            return View(values);
        }

        // GET: /Testimonial/CreateTestimonial
        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        // POST: /Testimonial/CreateTestimonial
        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial testimonial)
        {
            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Testimonial/DeleteTestimonial/5
        public IActionResult DeleteTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            if (value != null)
            {
                _context.Testimonials.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: /Testimonial/UpdateTestimonial/5
        [HttpGet]
        public IActionResult UpdateTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            if (value == null)
            {
                return RedirectToAction("Index"); // Kayıt bulunamazsa listeye dön
            }
            return View(value);
        }

        // POST: /Testimonial/UpdateTestimonial
        [HttpPost]
        public IActionResult UpdateTestimonial(Testimonial testimonial)
        {
            _context.Testimonials.Update(testimonial);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}