namespace BookService.Admin.Startup.Services;

public interface IUserProvider
{
    public string? Email { get; }
    public string? Role { get; }
    public string? Id { get; }
}