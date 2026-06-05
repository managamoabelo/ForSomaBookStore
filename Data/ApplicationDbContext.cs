using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ForSomaBookStore.Models;

namespace ForSomaBookStore.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Textbook> Textbooks { get; set; }
    public DbSet<WantedAd> WantedAds { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Review> Reviews { get; set; }

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
            .HasForeignKey(x => x.UserId);

        builder.Entity<Offer>()
            .HasOne(x => x.Textbook)
            .WithMany(x => x.Offers)
            .HasForeignKey(x => x.TextbookId);
    }
}