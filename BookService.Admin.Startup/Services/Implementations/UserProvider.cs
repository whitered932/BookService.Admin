using System.Security.Claims;

namespace BookService.Admin.Startup.Services.Implementations;

public class UserProvider(IHttpContextAccessor contextAccessor) : IUserProvider
{
    public string? Email => contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
    
    public string? Role => contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Role);

    public string? Id => contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
}