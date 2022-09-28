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
using BooksSpotLibrary.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BooksSpotLibrary.Pages.Books
{
    public class EditModel : DI_BasePageModel
    {
        public EditModel(
            BooksSpotLibraryContext libraryContext,
            ApplicationDbContext usersContext,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(libraryContext, usersContext, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || BooksContext.Book == null)
            {
                return NotFound();
            }

            var book =  await BooksContext.Book.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, Book,
                                                  OperationNames.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

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

            BooksContext.Attach(Book).State = EntityState.Modified;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, Book,
                                                 OperationNames.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            try
            {
                await BooksContext.SaveChangesAsync();
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
          return BooksContext.Book.Any(e => e.Id == id);
        }
    }
}
