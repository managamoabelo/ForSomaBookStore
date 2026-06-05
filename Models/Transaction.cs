using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace ForSomaBookStore.Models;

public class Transaction
{
    public int Id { get; set; }

    public int OfferId { get; set; }

    public Offer? Offer { get; set; }

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
}