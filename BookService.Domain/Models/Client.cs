namespace BookService.Domain.Models;

public class Client : BaseModel
{
    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
}