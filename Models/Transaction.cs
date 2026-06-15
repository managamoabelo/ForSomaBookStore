using System.ComponentModel.DataAnnotations;

namespace ForSomaBookStore.Models;

public class Transaction
{
    public int Id { get; set; }

    [Display(Name = "Offer ID")]
    public int OfferId { get; set; }

    public Offer Offer { get; set; } = null!;

    public TransactionStatus Status { get; set; }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    [Display(Name = "Meetup Location")]
    public string? MeetupLocation { get; set; }

    [Display(Name = "Payment Method")]
    public string? PaymentMethod { get; set; }

    [Display(Name = "Transaction Date")]
    public DateTime TransactionDate { get; set; }

    public bool Completed { get; set; }

    // Payment Gateway Fields

    [Display(Name = "Payment Completed")]
    public bool Paid { get; set; }

    [Display(Name = "Payment Date")]
    public DateTime? PaymentDate { get; set; }

    [Display(Name = "Payment Reference")]
    public string? PaymentReference { get; set; }
}