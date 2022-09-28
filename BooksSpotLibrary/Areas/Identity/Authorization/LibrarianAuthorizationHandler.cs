using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using BooksSpotLibrary.Models;
using BooksSpotLibrary.Constants;


namespace BooksSpotLibrary.Areas.Identity.Authorization
{
    public class LibrarianAuthorizationHandler
    :AuthorizationHandler<OperationAuthorizationRequirement, Book>
    {
        protected override Task HandleRequirementAsync(
                                    AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                    Book resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Librarians can do anything.
            if (context.User.IsInRole(RoleNames.LibrarianRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
