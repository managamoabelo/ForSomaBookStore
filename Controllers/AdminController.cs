using ForSomaBookStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForSomaBookStore.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public IActionResult Index()
    {
        ViewBag.Users =
            _context.Users.Count();

        ViewBag.Textbooks =
            _context.Textbooks.Count();

        ViewBag.Offers =
            _context.Offers.Count();

        ViewBag.Transactions =
            _context.Transactions.Count();

        return View();
    }
}