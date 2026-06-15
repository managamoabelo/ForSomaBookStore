using ForSomaBookStore.Data;
using ForSomaBookStore.Models;
using ForSomaBookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForSomaBookStore.Controllers;

[Authorize]
public class TextbooksController(ITextbookService service, ApplicationDbContext context) : Controller
{
    private readonly ITextbookService _service = service;
    private readonly ApplicationDbContext _context = context;

    public async Task<IActionResult> Index(string searchString)
    {
        var books = await _service.GetAllAsync();

        if (!string.IsNullOrEmpty(searchString))
        {
            books = [.. books.Where(t =>
                (t.Title?.Contains(searchString) ?? false) ||
                (t.Author?.Contains(searchString) ?? false) ||
                (t.ISBN?.Contains(searchString) ?? false))];
        }

        return View(books);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Textbook textbook)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(textbook);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            textbook.UserId = userId;

            await _service.CreateAsync(textbook);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);

            return View(textbook);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var textbook = await _service.GetByIdAsync(id);

        return View(textbook);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Textbook textbook)
    {
        if (!ModelState.IsValid)
            return View(textbook);

        // optional safety
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Challenge();
        }

        textbook.UserId = userId;

        await _service.UpdateAsync(textbook);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var textbook = await _service.GetByIdAsync(id);

        if (textbook == null)
            return NotFound();

        return View(textbook);
    }

    public async Task<IActionResult> MyListings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Login", "Account");

        var books = await _service.GetByUserIdAsync(userId);

        return View(books);
    }

    [HttpPost]
    public async Task<IActionResult> Report(int id, string reason)
    {
        var textbook = await _context.Textbooks.FindAsync(id);

        if (textbook == null)
        {
            return NotFound();
        }

        textbook.Reported = true;
        textbook.ReportReason = reason;

        await _context.SaveChangesAsync();

        TempData["Success"] = "Listing reported successfully.";

        return RedirectToAction(nameof(Details), new { id });
    }
}