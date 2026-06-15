using ForSomaBookStore.Data;
using ForSomaBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForSomaBookStore.Controllers;

[Authorize]
public class OffersController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var offers = await _context.Offers
            .Include(o => o.Textbook)
            .Where(o => o.Textbook != null && o.Textbook.UserId == userId)
            .ToListAsync();

        return View(offers);
    }

    public IActionResult Create(int textbookId)
    {
        ViewBag.TextbookId = textbookId;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Offer offer)
    {
        if (!ModelState.IsValid)
            return View(offer);

        offer.BuyerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        offer.Status = Offer.OfferStatus.Pending;

        _context.Offers.Add(offer);
        await _context.SaveChangesAsync();

        var textbook = await _context.Textbooks
            .FirstOrDefaultAsync(t => t.Id == offer.TextbookId);

        if (textbook != null)
        {
            _context.Notifications.Add(new Notification
            {
                UserId = textbook.UserId,
                Message = $"You received an offer on '{textbook.Title}'.",
                IsRead = false
            });

            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Accept(int id)
    {
        var offer = await _context.Offers
            .Include(o => o.Textbook)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (offer == null)
            return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (offer.Textbook?.UserId != userId)
            return Unauthorized();

        offer.Status = Offer.OfferStatus.Accepted;

        var transaction = new Transaction
        {
            OfferId = offer.Id,
            Status = Transaction.TransactionStatus.Pending,
            TransactionDate = DateTime.UtcNow,
            MeetupLocation = "Campus Library",
            PaymentMethod = "Cash",
            Completed = false
        };

        _context.Transactions.Add(transaction);

        offer.Textbook!.IsAvailable = false;

        _context.Notifications.Add(new Notification
        {
            UserId = offer.BuyerId,
            Message = $"Your offer for '{offer.Textbook?.Title}' was accepted.",
            IsRead = false
        });

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id)
    {
        var offer = await _context.Offers
            .Include(o => o.Textbook)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (offer == null)
            return NotFound();

        offer.Status = Offer.OfferStatus.Rejected;

        _context.Notifications.Add(new Notification
        {
            UserId = offer.BuyerId,
            Message = $"Your offer for '{offer.Textbook?.Title}' was declined.",
            IsRead = false
        });

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}