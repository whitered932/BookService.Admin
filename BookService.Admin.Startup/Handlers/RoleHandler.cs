using System.Security.Claims;
using BookService.Admin.Startup.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace BookService.Admin.Startup.Handlers;

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        var role = context.User.FindFirstValue(ClaimTypes.Role);
        if (requirement.Role == role)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}