using BooksSpotLibrary.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksSpotLibrary.Pages.Books
{
    public class DI_BasePageModel :PageModel
    {
        protected ApplicationDbContext UsersContext { get; }
        protected BooksSpotLibraryContext BooksContext { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }


        public DI_BasePageModel(
            BooksSpotLibraryContext booksContext,
            ApplicationDbContext usersContext,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base()
        {
            BooksContext = booksContext;
            UsersContext = usersContext;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }
    }
}
