using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksSpotLibrary.Data;
using BooksSpotLibrary.Models;

namespace BooksSpotLibrary.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly BooksSpotLibrary.Data.BooksSpotLibraryContext _context;

        public EditModel(BooksSpotLibrary.Data.BooksSpotLibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book =  await _context.Book.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(Guid id)
        {
          return _context.Book.Any(e => e.Id == id);
        }
    }
}
