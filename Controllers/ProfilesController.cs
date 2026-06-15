using ForSomaBookStore.Data;
using ForSomaBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForSomaBookStore.Controllers
{
    [Authorize]
    public class ProfilesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfilesController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();

            var booksCount = _context.Textbooks
                .Count(t => t.UserId == user.Id);

            var offersCount = _context.Offers
                .Count(o => o.BuyerId == user.Id);

            var transactionsCount = _context.Transactions
                .Count(t => t.Offer != null &&
                t.Offer.BuyerId == user.Id);

            var reviewsCount = _context.Reviews
                .Count(r => r.RevieweeId == user.Id);

            var recentListings = _context.Textbooks
                .Where(t => t.UserId == user.Id)
                .OrderByDescending(t => t.Id)
                .Take(5)
                .ToList();

            var recentReviews = _context.Reviews
                .Include(r => r.Reviewer)
                .Where(r => r.RevieweeId == user.Id)
                .OrderByDescending(r => r.Id)
                .Take(5)
                .ToList();

            var profile = new Profile
            {
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Institution = user.Campus,
                StudentNumber = user.StudentNumber,
                TrustScore = (int)user.TrustScore,
                Bio = $"Student at {user.Campus}",

                BooksListed = booksCount,
                OffersMade = offersCount,
                TransactionsCompleted = transactionsCount,
                ReviewsReceived = reviewsCount,

                RecentListings = recentListings,
                RecentReviews = recentReviews
            };

            return View(profile);
        }
    }
}