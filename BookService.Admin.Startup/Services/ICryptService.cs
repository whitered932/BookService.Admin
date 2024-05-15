namespace BookService.Admin.Startup.Services;

public interface ICryptService
{
    public string Hash(string password);
    public bool Verify(string password, string hashedPassword);
}