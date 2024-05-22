using Microsoft.AspNetCore.Authorization;

namespace BookService.Admin.Startup.Requirements;

public class RoleRequirement(string role) : IAuthorizationRequirement
{
    public string Role { get; } = role;
}