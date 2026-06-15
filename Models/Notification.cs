using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ForSomaBookStore.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="User ID")]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? Link { get; set; }
    }
}
