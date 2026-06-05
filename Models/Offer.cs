using ForSomaBookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace ForSomaBookStore.Models
{
    public class Offer
    {
        public int Id { get; set; }

        [Display(Name = "Offer Amount")]
        public decimal OfferAmount { get; set; }

        [Display(Name = "Offer Date")]
        public DateTime OfferDate { get; set; }
            = DateTime.UtcNow;

        public OfferStatus Status { get; set; }

        public enum OfferStatus
        {
            Pending,
            Accepted,
            Rejected
        }

        [Display(Name = "Textbook ID")]
        public int TextbookId { get; set; }

        public Textbook? Textbook { get; set; }

        [Display(Name = "Buyer ID")]
        public string? BuyerId { get; set; }

        public ApplicationUser? Buyer { get; set; }
    }
}