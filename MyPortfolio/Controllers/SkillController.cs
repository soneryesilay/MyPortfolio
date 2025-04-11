using Microsoft.AspNetCore.Authorization; // Login koruması için
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System.Linq;

namespace MyPortfolio.Controllers
{
    [Authorize] // Bu controller'a sadece giriş yapmış kullanıcılar erişebilir
    public class SkillController : Controller
    {
        private readonly MyPortfolioContext _context;

        public SkillController(MyPortfolioContext context)
        {
            _context = context;
        }

        // GET: /Skill/Index
        public IActionResult Index()
        {
            var values = _context.Skills.ToList();
            return View(values);
        }

        // GET: /Skill/CreateSkill
        [HttpGet]
        public IActionResult CreateSkill()
        {
            return View();
        }

        // POST: /Skill/CreateSkill
        [HttpPost]
        public IActionResult CreateSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Skill/DeleteSkill/5
        public IActionResult DeleteSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value != null)
            {
                _context.Skills.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: /Skill/UpdateSkill/5
        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value == null)
            {
                return RedirectToAction("Index"); // Kayıt bulunamazsa listeye dön
            }
            return View(value);
        }

        // POST: /Skill/UpdateSkill
        [HttpPost]
        public IActionResult UpdateSkill(Skill skill)
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}