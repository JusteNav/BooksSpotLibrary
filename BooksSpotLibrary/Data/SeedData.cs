using BooksSpotLibrary.Constants;
using Microsoft.EntityFrameworkCore;
using BooksSpotLibrary.Models;
using Microsoft.AspNetCore.Identity;
using BooksSpotLibrary.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;

namespace BooksSpotLibrary.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testLibrarianPw1, string testLibrarianPw2, string testUserPw1, string testUserPw2)
        {
            string librarianID1;
            string userID1;
            string userID2;

            { //populate users
                using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
                {
                    // Password is set with the following:
                    //dotnet user-secrets set SeedLibrarianPW1 "123Abc?"
                    //dotnet user-secrets set SeedLibrarianPW2 "456Def?"
                    //dotnet user-secrets set SeedUserPW1 "789Ghi?"
                    //dotnet user-secrets set SeedUserPW2 "123Jkl?"

                    librarianID1 = await EnsureUser(serviceProvider, testLibrarianPw1, "librarian1@test.com");
                    await EnsureRole(serviceProvider, librarianID1, RoleNames.LibrarianRole);
                    var librarianID2 = await EnsureUser(serviceProvider, testLibrarianPw2, "librarian2@test.com");
                    await EnsureRole(serviceProvider, librarianID2, RoleNames.LibrarianRole);

                    // allowed user can create and edit contacts that they create
                    userID1 = await EnsureUser(serviceProvider, testUserPw1, "user1@test.com");
                    await EnsureRole(serviceProvider, userID1, RoleNames.UserRole);

                    userID2 = await EnsureUser(serviceProvider, testUserPw2, "user2@test.com");
                    await EnsureRole(serviceProvider, userID2, RoleNames.UserRole);
                }
            }

            { //populate books
                using (var context = new BooksSpotLibraryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BooksSpotLibraryContext>>()))
                {
                    if (context == null || context.Book == null)
                    {
                        throw new ArgumentNullException("Null BooksSpotLibraryContext");
                    }

                    // Look for any books.
                    if (context.Book.Any())
                    {
                        return;   // DB has been seeded
                    }

                    var books1 = await GetBooksForSeeding(serviceProvider, 20, userID1);
                    var books2 = await GetBooksForSeeding(serviceProvider, 20, userID2);
                    var books3 = await GetBooksForSeeding(serviceProvider, 20, librarianID1);

                    context.Book.AddRange(books1);
                    context.Book.AddRange(books2);
                    context.Book.AddRange(books3);

                    await context.SaveChangesAsync();
                }
            }
        }


        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                };
               var IR = await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception($"The {UserName} password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public async static Task<Book[]> GetBooksForSeeding(IServiceProvider serviceProvider, int num, string userID)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var ret = new Book[num];
            Random rnd = new Random();
            var user = await userManager.FindByIdAsync(userID);

            DateTime today = DateTime.Today;
            for (int i = 0; i < num; i++)
            {
                ret[i] = new Book();

                ret[i].Id = Guid.NewGuid();
                ret[i].DateAdded = today;
                ret[i].Title = $"Title{i}";
                ret[i].Author = $"Author{i}";
                ret[i].Category = BookCategory.Categories[rnd.Next(0, 10)];
                ret[i].Publisher = Publisher.Publishers[rnd.Next(0, 5)];
                ret[i].PublishingDate = new DateTime(rnd.Next(1950, today.Year + 1), rnd.Next(1, 13), rnd.Next(1, 28));
                ret[i].ISBNCode = $"ISBN{i + 12}{i + 15}{i + 19}";
                ret[i].Status = Enum.GetName(typeof(BookStatus), rnd.Next(0, 3));
                ret[i].Borrower = ret[i].Status == BookStatus.Free.ToString() ? null : user.UserName.ToString();
            }

            return ret;
        }
    }
}
