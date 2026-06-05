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

        public string? ReviewerId { get; set; }

        public string? RevieweeId { get; set; }

        public int TransactionId { get; set; }

        public Transaction? Transaction { get; set; }
    }
}