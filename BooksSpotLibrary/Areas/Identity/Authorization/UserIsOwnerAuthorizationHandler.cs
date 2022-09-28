using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BooksSpotLibrary.Constants;
using BooksSpotLibrary.Models;

namespace BooksSpotLibrary.Areas.Identity.Authorization
{
    public class UserIsOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Book>
    {
        UserManager<IdentityUser> _userManager;

        public UserIsOwnerAuthorizationHandler(UserManager<IdentityUser>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Book resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            //Only owners of books can cancel reservations

            if (requirement.Name != OperationNames.CancelReservationOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.Borrower == _userManager.GetUserName(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }
}
