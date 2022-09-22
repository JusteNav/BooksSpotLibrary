using System.ComponentModel.DataAnnotations;

namespace BooksSpotLibrary.Models
{
    public class Book
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        [Required]
        public string ISBNCode { get; set; }

        [Required]
        public string Status { get; set; }

        public string? Borrower { get; set; }

    }
}
