using ForSomaBookStore.Data;
using ForSomaBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForSomaBookStore.Controllers;

[Authorize]
public class ReviewsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReviewsController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var reviews = await _context.Reviews
            .Include(r => r.Reviewer)
            .Include(r => r.Reviewee)
            .Include(r => r.Transaction)
            .OrderByDescending(r => r.Id)
            .ToListAsync();

        return View(reviews);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Review review)
    {
        // A review must belong to a real transaction, otherwise the database
        // rejects it (TransactionId is a required foreign key).
        var transactionExists = review.TransactionId > 0
            && await _context.Transactions.AnyAsync(t => t.Id == review.TransactionId);

        if (!transactionExists)
            ModelState.AddModelError(string.Empty, "A valid transaction is required to leave a review.");

        if (!ModelState.IsValid)
            return View(review);

        review.ReviewerId =
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        _context.Reviews.Add(review);

        if (!string.IsNullOrEmpty(review.RevieweeId))
        {
            var reviewee =
                await _userManager.FindByIdAsync(review.RevieweeId);

            if (reviewee != null)
            {
                reviewee.TrustScore += review.Rating;
                await _userManager.UpdateAsync(reviewee);
            }
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult LeaveReview(int transactionId, string revieweeId)
    {
        var review = new Review
        {
            TransactionId = transactionId,
            RevieweeId = revieweeId
        };

        return View(review);
    }
}