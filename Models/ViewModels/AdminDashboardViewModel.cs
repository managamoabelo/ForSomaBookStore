using ForSomaBookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace ForSomaBookStore.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        [Display(Name ="User Count")]
        public int UserCount { get; set; }

        [Display(Name = "Textbook Count")]
        public int TextbookCount { get; set; }

        [Display(Name = "Offer Count")]
        public int OfferCount { get; set; }

        [Display(Name = "Transaction Count")]
        public int TransactionCount { get; set; }

        [Display(Name = "Review Count")]
        public int ReviewCount { get; set; }

        [Display(Name = "Contact Message Count")]
        public int ContactMessageCount { get; set; }

        [Display(Name = "Reported Listings Count")]
        public int ReportedListingsCount { get; set; }

        public List<Review> Reviews { get; set; } = [];

        public List<ApplicationUser> Users { get; set; } = [];
        public List<Textbook> Textbooks { get; set; } = [];
        public List<Transaction> Transactions { get; set; } = [];
    }
}