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
using BooksSpotLibrary.Logic;
using Microsoft.AspNetCore.Http;

namespace BooksSpotLibrary.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BooksSpotLibraryContext _context;
        public IndexModel(BooksSpotLibraryContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Selections { get; set; } = new SelectList(UIConstants.Selections);

        [BindProperty(SupportsGet = true)]
        public string? Selection { get; set; }

        public string[] Statuses { get; set; }

        public string[] Categories { get; set; }

        public async Task OnGetAsync()
        {
            var books = from m in _context.Book
                        select m;

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
            Statuses = Request.Query["BookStatus"];
            if (Statuses == null || Statuses.Count() == 0)
            {

            }

            else
            {
                books = books.Where(x => Statuses.Contains(x.Status));
            }

            Categories = Request.Query["Category"];
            if (Categories == null || Categories.Count() == 0)
            {

            }

            else
            {
                books = books.Where(x => Categories.Contains(x.Category));
            }
            Book = await books.ToListAsync();
        }
    }
}
