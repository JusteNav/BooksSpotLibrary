using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BooksSpotLibrary.Data;
using BooksSpotLibrary.Models;

namespace BooksSpotLibrary.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly BooksSpotLibrary.Data.BooksSpotLibraryContext _context;

        public DeleteModel(BooksSpotLibrary.Data.BooksSpotLibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }
            var book = await _context.Book.FindAsync(id);

            if (book != null)
            {
                Book = book;
                _context.Book.Remove(Book);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
