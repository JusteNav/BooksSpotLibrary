using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BooksSpotLibrary.Models;
using BooksSpotLibrary.Logic;

namespace BooksSpotLibrary.Data
{
    public class BooksSpotLibraryContext : DbContext
    {
        public BooksSpotLibraryContext (DbContextOptions<BooksSpotLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasData(BookHelper.GetBooksForSeeding(40));
        }
    }
}
