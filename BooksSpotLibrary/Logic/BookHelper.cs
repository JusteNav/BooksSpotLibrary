using BooksSpotLibrary.Enums;
using BooksSpotLibrary.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace BooksSpotLibrary.Logic
{
    public class BookHelper
    {
        public static Book[] GetBooksForSeeding(int num)
        {
            var ret = new Book[num];
            Random rnd = new Random();
            DateTime today = DateTime.Today;
            for (int i = 0; i < num; i++)
            {
                ret[i] = new Book
                {
                    Id = Guid.NewGuid(),
                    DateAdded = today,
                    Title = $"Title{i}",
                    Author = $"Author{i}",
                    Category = Enum.GetValues<Category>()[rnd.Next(0, 10)].ToString(),
                    Publisher = $"Publisher{i}",
                    PublishingDate = new DateTime(rnd.Next(1920, today.Year + 1), rnd.Next(1, 13), rnd.Next(1, 28)),
                    ISBNCode = $"ISBN{i}{i + 1}{i+6}",
                    Status = BookStatus.Free.ToString(),
                    Borrower = null
                };
            }
            return ret;
        }
    }
}
