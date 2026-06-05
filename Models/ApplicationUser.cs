using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using ForSomaBookStore.Models;

namespace ForSomaBookStore.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(50)]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    public string? Campus { get; set; }

    [Display(Name = "Trust Score")]
    public decimal TrustScore { get; set; }

    [Display(Name = "Preferred Language")]
    public string? PreferredLanguage { get; set; } = "English";

    public ICollection<Textbook> Textbooks { get; set; }
    = [];

    public ICollection<Offer> Offers { get; set; }
    = [];
}