using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BooksSpotLibrary.Models;

namespace BooksSpotLibrary.Data
{
    public class BooksSpotLibraryContext : DbContext
    {
        public BooksSpotLibraryContext (DbContextOptions<BooksSpotLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;

    }
}
