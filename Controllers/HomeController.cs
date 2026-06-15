using ForSomaBookStore.Data;
using ForSomaBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ForSomaBookStore.Controllers
{
    public class HomeController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IActionResult> Index(string search, string condition)
        {
            var books = _context.Textbooks.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(x =>
                    (x.Title ?? "").Contains(search) ||
                    (x.Author ?? "").Contains(search) ||
                    (x.ISBN ?? "").Contains(search));
            }

            if (!string.IsNullOrEmpty(condition))
            {
                books = books.Where(x =>
                    x.Condition == condition);
            }

            ViewBag.Users = _context.Users.Count();

            ViewBag.Textbooks = _context.Textbooks.Count();

            ViewBag.Transactions = _context.Transactions.Count();

            var featuredBooks = _context.Textbooks
                .Where(t => t.IsAvailable)
                .OrderByDescending(t => t.Id)
                .Take(6)
                .ToList();

            ViewBag.FeaturedBooks = featuredBooks;

            return View(await books.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMessage ContactMessage)
        {
            if (!ModelState.IsValid)
            {
                return View(ContactMessage);
            }

            _context.ContactMessages.Add(ContactMessage);

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Thank you for contacting us. Your message has been submitted successfully.";

            return RedirectToAction(nameof(Contact));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}