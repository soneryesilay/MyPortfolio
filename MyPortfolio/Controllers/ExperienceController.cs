using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context; // Context için using
using MyPortfolio.DAL.Entities;
using System.Linq; // ToList, Find gibi metotlar için

namespace MyPortfolio.Controllers // Namespace'i kontrol et (MyPortolio değil MyPortfolio olmalı gibi duruyor)
{
    [Authorize]
    public class ExperienceController : Controller
    {
        // Context'i tutacak private readonly field
        private readonly MyPortfolioContext _context;

        // Constructor Injection ile MyPortfolioContext'i al
        public ExperienceController(MyPortfolioContext context)
        {
            _context = context; // Gelen context'i private field'a ata
        }

        public IActionResult ExperienceList()
        {
            // _context field'ını kullan
            var values = _context.Experiences.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateExperience(Experience experience)
        {
            // _context field'ını kullan
            _context.Experiences.Add(experience);
            _context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }

        public IActionResult DeleteExperience(int id)
        {
            // _context field'ını kullan
            var value = _context.Experiences.Find(id);
            if (value != null) // Silmeden önce null kontrolü eklemek iyi bir pratiktir
            {
                _context.Experiences.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("ExperienceList");
        }

        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            // _context field'ını kullan
            var value = _context.Experiences.Find(id);
            if (value == null)
            {
                // Eğer kayıt bulunamazsa, liste sayfasına yönlendirilebilir veya NotFound döndürülebilir.
                return RedirectToAction("ExperienceList");
                // return NotFound(); // Alternatif
            }
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateExperience(Experience experience)
        {
            // _context field'ını kullan
            _context.Experiences.Update(experience);
            _context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }
    }
}