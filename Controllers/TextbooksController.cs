using ForSomaBookStore.Models;
using ForSomaBookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForSomaBookStore.Controllers;

[Authorize]
public class TextbooksController(ITextbookService service) : Controller
{
    private readonly ITextbookService _service = service;

    public async Task<IActionResult> Index()
    {
        return View(await _service.GetAllAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Textbook textbook)
    {
        if (ModelState.IsValid)
        {
            // 🔥 FIX: attach logged-in user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            textbook.UserId = userId;

            await _service.CreateAsync(textbook);
            return RedirectToAction(nameof(Index));
        }

        return View(textbook);
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

        // optional safety (recommended)
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        textbook.UserId = userId;

        await _service.UpdateAsync(textbook);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}