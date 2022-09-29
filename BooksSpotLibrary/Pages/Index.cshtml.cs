using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BooksSpotLibrary.Data;
using BooksSpotLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using BooksSpotLibrary.Pages.Books;
using Microsoft.AspNetCore.Identity;

namespace BooksSpotLibrary.Pages
{
    [AllowAnonymous]
    public class IndexModel : DI_BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
        BooksSpotLibraryContext libraryContext,
        ApplicationDbContext usersContext,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager, 
        ILogger<IndexModel> logger) : base(libraryContext, usersContext, authorizationService, userManager)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}