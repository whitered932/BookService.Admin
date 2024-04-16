namespace BookService.Domain.Models;

public class Client : BaseModel
{
    public Client(string name, string phoneNumber, string email)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
    }


    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
}