using ForSomaBookStore.Data;
using ForSomaBookStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForSomaBookStore.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    // Dashboard
    public async Task<IActionResult> Index()
    {
        var vm = new AdminDashboardViewModel
        {
            UserCount = _context.Users.Count(),
            TextbookCount = _context.Textbooks.Count(),
            OfferCount = _context.Offers.Count(),
            TransactionCount = _context.Transactions.Count(),
            ReviewCount = _context.Reviews.Count(),
            ContactMessageCount = _context.ContactMessages.Count(),
            ReportedListingsCount = _context.Textbooks.Count(t => t.Reported),

            Users = await _context.Users.ToListAsync(),

            Textbooks = await _context.Textbooks
                .Include(t => t.User)
                .ToListAsync(),

            Transactions = await _context.Transactions
                .Include(t => t.Offer!)
                .ThenInclude(o => o.Textbook)
                .ToListAsync(),

            Reviews = await _context.Reviews
                .ToListAsync()
        };

        return View(vm);
    }

    // CONTACT MESSAGES

    public async Task<IActionResult> ContactMessages()
    {
        var messages = await _context.ContactMessages
            .OrderByDescending(x => x.DateSubmitted)
            .ToListAsync();

        return View(messages);
    }

    public async Task<IActionResult> ResolveMessage(int id)
    {
        var message = await _context.ContactMessages.FindAsync(id);

        if (message == null)
            return NotFound();

        message.Resolved = true;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(ContactMessages));
    }

    public async Task<IActionResult> DeleteMessage(int id)
    {
        var message = await _context.ContactMessages.FindAsync(id);

        if (message == null)
            return NotFound();

        _context.ContactMessages.Remove(message);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(ContactMessages));
    }

    // REPORTED LISTINGS

    public async Task<IActionResult> ReportedListings()
    {
        var books = await _context.Textbooks
            .Include(x => x.User)
            .Where(x => x.Reported)
            .ToListAsync();

        return View(books);
    }

    public async Task<IActionResult> ReviewReport(int id)
    {
        var book = await _context.Textbooks.FindAsync(id);

        if (book == null)
            return NotFound();

        book.ReportReviewed = true;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(ReportedListings));
    }

    public async Task<IActionResult> RemoveListing(int id)
    {
        var book = await _context.Textbooks.FindAsync(id);

        if (book == null)
            return NotFound();

        _context.Textbooks.Remove(book);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(ReportedListings));
    }
}