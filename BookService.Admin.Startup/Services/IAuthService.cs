namespace BookService.Admin.Startup.Services;

public interface IAuthService
{
    public Task Authorize(string email, string name, string role, string id, DateTime expiredAtUtc);

}