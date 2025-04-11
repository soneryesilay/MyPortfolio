using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System.Linq;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class FeatureController : Controller
    {
        private readonly MyPortfolioContext _context;

        public FeatureController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.Features.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateFeature()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFeature(Feature feature)
        {
            _context.Features.Add(feature);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteFeature(int id)
        {
            var value = _context.Features.Find(id);
            if (value != null)
            {
                _context.Features.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateFeature(int id)
        {
            var value = _context.Features.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateFeature(Feature feature)
        {
            _context.Features.Update(feature);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}