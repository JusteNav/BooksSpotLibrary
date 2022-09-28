using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BooksSpotLibrary.Data;
using BooksSpotLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using BooksSpotLibrary.Constants;
using System.Runtime.CompilerServices;

namespace BooksSpotLibrary.Pages.Books
{
    public class DetailsModel : DI_BasePageModel
    {
        private bool wantToReturn;
        [BindProperty(SupportsGet = true)]
        [HiddenInput]
        public bool WantToReturn { get { return wantToReturn; } set { wantToReturn = true; } }

        private bool wantToBorrow = false;
        [BindProperty(SupportsGet = true)]
        [HiddenInput]
        public bool WantToBorrow { get { return wantToBorrow; } set { wantToBorrow = true; } }

        private bool wantToReserve;
        [BindProperty(SupportsGet = true)]
        [HiddenInput]
        public bool WantToReserve { get { return wantToReserve; } set { wantToReserve = true; } }

        private bool wantToCancelReservation;
        [BindProperty(SupportsGet = true)]
        [HiddenInput]
        public bool WantToCancelReservation { get { return wantToCancelReservation; } set { wantToCancelReservation = true; } }


        public DetailsModel(
            BooksSpotLibraryContext libraryContext,
            ApplicationDbContext usersContext,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(libraryContext, usersContext, authorizationService, userManager)
        {
        }

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
                                                     OperationNames.Details);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            var book = await BooksContext.Book.FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            if (WantToReturn)
            {
                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                          User, book,
                                                          OperationNames.Return);
                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                book.Status = BookStatus.Free.ToString();
                book.Borrower = null;
                wantToReturn = false;
                await BooksContext.SaveChangesAsync();
            }

            else if (WantToBorrow)
            {
                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                          User, book,
                                                          OperationNames.Borrow);
                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                book.Status = BookStatus.Borrowed.ToString();
                book.Borrower = User.Identity.Name.ToString();
                wantToBorrow = false;
                await BooksContext.SaveChangesAsync();
            }
            else if (WantToReserve)
            {
                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                          User, book,
                                                          OperationNames.Reserve);
                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                book.Status = BookStatus.Reserved.ToString();
                book.Borrower = User.Identity.Name.ToString();
                wantToReserve = false;
                await BooksContext.SaveChangesAsync();

            }

            else if (WantToCancelReservation)
            {
                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                          User, book,
                                          OperationNames.CancelReservation);
                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                book.Status = BookStatus.Free.ToString();
                book.Borrower = null;
                wantToCancelReservation = false;
                await BooksContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

