using System.ComponentModel.DataAnnotations;

namespace ForSomaBookStore.Models;

public class Textbook
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string? Title { get; set; }

    [Required]
    public string? Author { get; set; }

    public string? ISBN { get; set; }

    [Display(Name = "Course Code")]
    public string? CourseCode { get; set; }

    public string? Edition { get; set; }

    public string? Condition { get; set; }

    [Range(0, 99999)]
    public decimal Price { get; set; }

    public string? Campus { get; set; }
    
    [Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }

    public bool IsAvailable { get; set; } = true;

    [Display(Name = "Date Created")]
    public DateTime DateCreated { get; set; }
        = DateTime.UtcNow;

    public string? UserId { get; set; }

    public required ApplicationUser User { get; set; }

    public required ICollection<Offer> Offers { get; set; }
}