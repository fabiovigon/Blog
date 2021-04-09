using Blog.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Authorization
{
    public class PostAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Post>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PostAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Post resource)
        {
            var applicationUser = await _userManager.GetUserAsync(context.User);

            if ((requirement.Name == Operations.Update.Name || requirement.Name == Operations.Delete.Name ) && applicationUser == resource.Creator)
            {
                context.Succeed(requirement);
            }
        }
    }
}
