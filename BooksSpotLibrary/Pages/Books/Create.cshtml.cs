using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BooksSpotLibrary.Data;
using BooksSpotLibrary.Models;
using BooksSpotLibrary.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BooksSpotLibrary.Pages.Books
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(
            BooksSpotLibraryContext libraryContext,
            ApplicationDbContext usersContext,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(libraryContext, usersContext, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                          User, Book,
                                                          OperationNames.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            BooksContext.Book.Add(Book);
            await BooksContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
