using Microsoft.AspNetCore.Authorization; // Login koruması için
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System.Linq;

namespace MyPortfolio.Controllers
{
    [Authorize] // Bu controller'a sadece giriş yapmış kullanıcılar erişebilir
    public class ContactController : Controller
    {
      private readonly MyPortfolioContext _context;

        public ContactController(MyPortfolioContext context)
        {
            _context = context;
        }

        // GET: /Contact/Index
        public IActionResult Index()
        {
            var values = _context.Contacts.ToList();
            return View(values);
        }

        // GET: /Contact/CreateContact
        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }

        // POST: /Contact/CreateContact
        [HttpPost]
        public IActionResult CreateContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Contact/DeleteContact/5
        public IActionResult DeleteContact(int id)
        {
            var value = _context.Contacts.Find(id);
            if (value != null)
            {
                _context.Contacts.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: /Contact/UpdateContact/5
        [HttpGet]
        public IActionResult UpdateContact(int id)
        {
            var value = _context.Contacts.Find(id);
            if (value == null)
            {
                return RedirectToAction("Index"); // Kayıt bulunamazsa listeye dön
            }
            return View(value);
        }

        // POST: /Contact/UpdateContact
        [HttpPost]
        public IActionResult UpdateContact(Contact contact)
        {
            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}