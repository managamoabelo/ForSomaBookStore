using System.ComponentModel.DataAnnotations;

namespace ForSomaBookStore.Models
{
    public class WantedAd
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Display(Name = "Date Posted")]
        public DateTime DatePosted { get; set; }
            = DateTime.UtcNow;

        public string? UserId { get; set; }

        public ApplicationUser? User { get; set; }
    }
}