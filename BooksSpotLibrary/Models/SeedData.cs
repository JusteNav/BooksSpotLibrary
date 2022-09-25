using BooksSpotLibrary.Data;
using BooksSpotLibrary.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BooksSpotLibrary.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BooksSpotLibraryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BooksSpotLibraryContext>>()))
            {
                if (context == null || context.Book == null)
                {
                    throw new ArgumentNullException("Null BooksSpotLibraryContext");
                }
                
                // Look for any books.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(GetBooksForSeeding(60));
                context.SaveChanges();
            }
        }

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
                    Category = BookCategory.Categories[rnd.Next(0, 10)],
                    Publisher = Publisher.Publishers[rnd.Next(0, 5)],
                    PublishingDate = new DateTime(rnd.Next(1950, today.Year + 1), rnd.Next(1, 13), rnd.Next(1, 28)),
                    ISBNCode = $"ISBN{i+12}{i + 15}{i + 19}",
                    Status = BookStatus.Free.ToString(),
                    Borrower = null
                };
            }
            return ret;
        }
    }
}
