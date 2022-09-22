using BooksSpotLibrary.Models;
using BooksSpotLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksSpotLibrary.Logic
{
    public class Queries
    {
        //public static int GetBookCount()
        //{
        //    using (BooksSpotLibraryContext db = new())
        //    {
        //        IQueryable<Book>? ids = db.Books;

        //        if (ids == null)
        //        {
        //            return 0;
        //        }

        //        else
        //        {
        //            return ids.Count();
        //        }
        //    }
        //}
        //public static IEnumerable<Book> GetNumberOfBooks(int number)
        //{
        //    using (BooksSpotLibraryContext db = new())
        //    {
        //        var ret = new List<Book>();

        //        IQueryable<Book>? ids = db.Books;

        //        if (ids == null)
        //        {
        //            return ret;
        //        }
        //        else if (ids.Count() < number)
        //        {
        //            return ids.ToList();
        //        }
        //        else
        //        {
        //            return ids.Take(number).ToList();
        //        }
        //    }
        //}
        //public static IEnumerable<Book> SearchBooks(string query, int number)
        //{
        //    using (BooksSpotLibraryContext db = new())
        //    {
        //        var ret = new List<Book>();

        //        IQueryable<Book>? ids = db.Books;

        //        if (ids == null)
        //        {
        //            return ret;
        //        }
        //        else
        //        {
        //            ret = ids.Where(x => x.Title.Contains(query) || x.Author.Contains(query) || x.Category.Contains(query)
        //            || x.PublishingDate.ToString().Contains(query) || x.Publisher.Contains(query) || x.ISBNCode.Contains(query)).ToList();
        //        }

        //        if(ret.Count < number)
        //        {
        //            return ret;
        //        }
        //        else
        //        {
        //            return ret.Take(number);
        //        }
        //    }
        //}


    }
}
