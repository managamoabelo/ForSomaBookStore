using ForSomaBookStore.Data;
using ForSomaBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForSomaBookStore.Controllers;

[Authorize]
public class OffersController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        return View(await _context.Offers
            .Include(o => o.Textbook)
            .ToListAsync());
    }

    public IActionResult Create(int textbookId)
    {
        ViewBag.TextbookId = textbookId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Offer offer)
    {
        if (!ModelState.IsValid)
            return View(offer);

        _context.Offers.Add(offer);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}