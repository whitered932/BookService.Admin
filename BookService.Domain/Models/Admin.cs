namespace BookService.Domain.Models;

public class Admin : BaseModel
{
    private Admin(string email, string password)
    {
        Email = email;
        Password = password;
    }
    
    public string Email { get; private set; }
    public string Password { get; private set; }
}