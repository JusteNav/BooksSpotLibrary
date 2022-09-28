using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using BooksSpotLibrary.Models;
using BooksSpotLibrary.Constants;


namespace BooksSpotLibrary.Areas.Identity.Authorization
{
    public class UserAuthorizationHandler
    :AuthorizationHandler<OperationAuthorizationRequirement, Book>
    {
        protected override Task HandleRequirementAsync(
                                    AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                    Book resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != OperationNames.BorrowOperationName 
                && requirement.Name != OperationNames.ReserveOperationName
                && requirement.Name != OperationNames.ViewDetailsOperationName)
            {
                return Task.CompletedTask;
            }

            //Users can view books, reserve or borrow
            if (context.User.IsInRole(RoleNames.UserRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
