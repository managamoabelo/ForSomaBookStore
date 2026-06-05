using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public string? Bio { get; set; }

        public IEnumerable<Textbook>? Textbooks { get; set; }

        [Display(Name = "Wanted Ads")]
        public IEnumerable<WantedAd>? WantedAds { get; set; }

        public IEnumerable<Transaction>? Transactions { get; set; }
    }
}