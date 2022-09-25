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
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Author { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Publishing Date")]
        public DateTime PublishingDate { get; set; }

        [Required]
        [Display(Name = "ISBN Code")]
        public string ISBNCode { get; set; }

        [Required]
        public string Status { get; set; }

        public string? Borrower { get; set; }

    }
}
