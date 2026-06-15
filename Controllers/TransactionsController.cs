using ForSomaBookStore.Data;
using ForSomaBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForSomaBookStore.Controllers;

[Authorize]
public class TransactionsController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        var transactions = await _context.Transactions
            .Include(t => t.Offer)
            .ThenInclude(o => o.Textbook)
            .ToListAsync();

        return View(transactions);
    }

    public async Task<IActionResult> Details(int id)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (transaction == null)
            return NotFound();

        return View(transaction);
    }

    public async Task<IActionResult> Accept(int id)
    {
        var offer = await _context.Offers
            .Include(o => o.Textbook)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (offer == null)
            return NotFound();

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

        offer.Textbook?.IsAvailable = false;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Complete(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);

        if (transaction == null)
            return NotFound();

        transaction.Status = Transaction.TransactionStatus.Completed;
        transaction.Completed = true;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Pay(int id)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Offer)
            .ThenInclude(o => o.Textbook)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transaction == null)
            return NotFound();

        return View(transaction);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PayConfirmed(int id)
    {
        var transaction = await _context.Transactions
            .FindAsync(id);

        if (transaction == null)
            return NotFound();

        transaction.Paid = true;

        transaction.PaymentDate = DateTime.Now;

        transaction.PaymentMethod = "Demo Payment Gateway";

        transaction.PaymentReference =
            Guid.NewGuid().ToString()[..8].ToUpper();

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Details),
                                new { id = transaction.Id });
    }
}