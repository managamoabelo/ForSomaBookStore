using ForSomaBookStore.Models;
using ForSomaBookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Create(Textbook textbook)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateAsync(textbook);
            return RedirectToAction(nameof(Index));
        }

        return View(textbook);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var textbook = await _service.GetByIdAsync(id);

        if (textbook == null)
            return NotFound();

        return View(textbook);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Textbook textbook)
    {
        if (!ModelState.IsValid)
            return View(textbook);

        await _service.UpdateAsync(textbook);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}