using ForSomaBookStore.Data;
using ForSomaBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForSomaBookStore.Controllers;

[Authorize]
public class WantedAdsController : Controller
{
    private readonly ApplicationDbContext _context;

    public WantedAdsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.WantedAds.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(WantedAd wantedAd)
    {
        if (!ModelState.IsValid)
            return View(wantedAd);

        _context.WantedAds.Add(wantedAd);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var ad = await _context.WantedAds.FindAsync(id);

        if (ad != null)
        {
            _context.WantedAds.Remove(ad);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}