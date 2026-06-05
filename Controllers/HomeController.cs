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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index(
            string search,
            string condition)
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

            return View(await books.ToListAsync());
        }
    }
}
