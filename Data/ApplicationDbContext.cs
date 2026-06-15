using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ForSomaBookStore.Models;

namespace ForSomaBookStore.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Textbook> Textbooks { get; set; }
    public DbSet<WantedAd> WantedAds { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Textbook>()
            .HasIndex(x => x.Title);

        builder.Entity<Textbook>()
            .HasIndex(x => x.ISBN);

        builder.Entity<Textbook>()
            .HasOne(x => x.User)
            .WithMany(x => x.Textbooks)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Offer>()
            .HasOne(o => o.Buyer)
            .WithMany()
            .HasForeignKey(o => o.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Offer>()
            .HasOne(x => x.Textbook)
            .WithMany(x => x.Offers)
            .HasForeignKey(x => x.TextbookId);

        // Decimal precision
        builder.Entity<ApplicationUser>()
            .Property(x => x.TrustScore)
            .HasPrecision(18, 2);

        builder.Entity<Textbook>()
            .Property(x => x.Price)
            .HasPrecision(18, 2);

        builder.Entity<Offer>()
            .Property(x => x.OfferAmount)
            .HasPrecision(18, 2);

        builder.Entity<Review>()
            .HasOne(r => r.Reviewer)
            .WithMany()
            .HasForeignKey(r => r.ReviewerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Review>()
            .HasOne(r => r.Reviewee)
            .WithMany()
            .HasForeignKey(r => r.RevieweeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}