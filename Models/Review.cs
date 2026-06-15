using ForSomaBookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace ForSomaBookStore.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(500)]
        public string? Comment { get; set; }

        [Display(Name = "Reviewer ID")]
        public string? ReviewerId { get; set; }

        public ApplicationUser? Reviewer { get; set; }

        [Display(Name = "Reviewee ID")]
        public string? RevieweeId { get; set; }

        public ApplicationUser? Reviewee { get; set; }

        [Display(Name = "Transaction ID")]
        public int TransactionId { get; set; }

        public Transaction? Transaction { get; set; }
    }
}