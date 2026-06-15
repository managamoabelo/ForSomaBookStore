using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ForSomaBookStore.Models;

namespace ForSomaBookStore.Models
{
    public class Profile
    {
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Institution { get; set; }

        [Display(Name = "Student Number")]
        public string? StudentNumber { get; set; }

        [Display(Name = "Trust Score")]
        public int TrustScore { get; set; }

        public string? Bio { get; set; }

        [Display(Name = "Books Listed")]
        public int BooksListed { get; set; }

        [Display(Name = "Offers Made")]
        public int OffersMade { get; set; }

        [Display(Name = "Transactions Completed")]
        public int TransactionsCompleted { get; set; }

        [Display(Name = "Reviews Received")]
        public int ReviewsReceived { get; set; }

        [Display(Name = "Recent Listings")]
        public List<Textbook> RecentListings { get; set; } = new();

        [Display(Name = "Recent Reviews")]
        public List<Review> RecentReviews { get; set; } = new();

        public IEnumerable<Textbook>? Textbooks { get; set; }
        public IEnumerable<WantedAd>? WantedAds { get; set; }
        public IEnumerable<Transaction>? Transactions { get; set; }
    }
}