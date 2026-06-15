using ForSomaBookStore.Data;
using ForSomaBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForSomaBookStore.Controllers;

[Authorize]
public class WantedAdsController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        var ads = await _context.WantedAds
            .Include(w => w.User)
            .OrderByDescending(w => w.DatePosted)
            .ToListAsync();

        return View(ads);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WantedAd wantedAd)
    {
        if (!ModelState.IsValid)
            return View(wantedAd);

        wantedAd.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        wantedAd.DatePosted = DateTime.UtcNow;

        _context.WantedAds.Add(wantedAd);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var ad = await _context.WantedAds.FindAsync(id);

        if (ad == null)
            return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (ad.UserId != userId && !User.IsInRole("Admin"))
            return Forbid();

        _context.WantedAds.Remove(ad);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}