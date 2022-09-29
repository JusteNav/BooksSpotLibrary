using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BooksSpotLibrary.Data;
using BooksSpotLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BooksSpotLibrary.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace BooksSpotLibrary.Pages.Books
{

    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(
            BooksSpotLibraryContext libraryContext,
            ApplicationDbContext usersContext,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(libraryContext, usersContext, authorizationService, userManager)
        {
        }

        public List<Book> Book { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Selections { get; set; } = new SelectList(UIConstants.Selections);

        [BindProperty(SupportsGet = true)]
        public string? Selection { get; set; }

        public string[] Statuses { get; set; }

        public string[] Categories { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            var books = from m in BooksContext.Book
                        select m;

            var currentUserName = UserManager.GetUserName(User);


            if (!string.IsNullOrEmpty(SearchString))
            {

                if (!string.IsNullOrEmpty(Selection))
                {
                    switch (Selection)
                    {
                        case "All":
                            {
                                books = books.Where(s => s.Title.Contains(SearchString) || s.Author.Contains(SearchString) || s.Category.Contains(SearchString) ||
                                s.Publisher.Contains(SearchString) || s.PublishingDate.ToString().Contains(SearchString) || s.ISBNCode.Contains(SearchString));
                                break;
                            }
                        case "Title":
                            {
                                books = books.Where(s => s.Title.Contains(SearchString));
                                break;
                            }
                        case "Author":
                            {
                                books = books.Where(s => s.Author.Contains(SearchString));
                                break;
                            }
                        case "Category":
                            {
                                books = books.Where(s => s.Category.Contains(SearchString));
                                break;
                            }
                        case "Publisher":
                            {
                                books = books.Where(s => s.Publisher.Contains(SearchString));
                                break;
                            }
                        case "Publishing Date":
                            {
                                books = books.Where(s => s.PublishingDate.ToString().Contains(SearchString));
                                break;
                            }
                        case "ISBN Code":
                            {
                                books = books.Where(s => s.ISBNCode.Contains(SearchString));
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }

            Categories = Request.Query["Category"];

            if (Categories != null && Categories.Count() != 0)
            {
                books = books.Where(x => Categories.Contains(x.Category));
            }

            Statuses = Request.Query["BookStatus"];
            string[] Statuses3;

            if (Statuses != null && Statuses.Count() != 0 && Statuses.Contains(UIConstants.Status[3]))
            {
                books = books.Where(x => x.Borrower == currentUserName);
                IEnumerable<string> Statuses2 = Statuses.Where(x => x != UIConstants.Status[3]);
                Statuses3 = Statuses2.ToArray();
            }
            else
            {
                Statuses3 = Statuses;
            }

            if (Statuses3 != null && Statuses3.Count() != 0)
            {
                books = books.Where(x => Statuses3.Contains(x.Status));
            }

            Book = await books.ToListAsync();
        }
    }
}
