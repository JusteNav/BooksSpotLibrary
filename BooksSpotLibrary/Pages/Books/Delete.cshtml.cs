using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BooksSpotLibrary.Data;
using BooksSpotLibrary.Constants;
using BooksSpotLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BooksSpotLibrary.Pages.Books
{
    public class DeleteModel : DI_BasePageModel
    {

        public DeleteModel(
            BooksSpotLibraryContext libraryContext,
            ApplicationDbContext usersContext,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(libraryContext, usersContext, authorizationService, userManager)
        {
        }

        [BindProperty]
      public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || BooksContext.Book == null)
            {
                return NotFound();
            }

            var book = await BooksContext.Book.FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, Book,
                                                 OperationNames.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || BooksContext.Book == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                     User, Book,
                                     OperationNames.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            var book = await BooksContext.Book.FindAsync(id);

            if (book != null)
            {
                Book = book;
                BooksContext.Book.Remove(Book);
                await BooksContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
