using System.Security.Cryptography;

namespace BookService.Domain.Models;

public class Employee : BaseModel
{
    private Employee() {}
    
    public Employee(string name, string phoneNumber, string email, string position, long restaurantId, string password)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Position = position;
        RestaurantId = restaurantId;
        Password = password;
    }

    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Position { get; private set; }
    public string Password { get; private set; }
    
    public long RestaurantId { get; private set; }

    public void Update(string name, string phoneNumber, string email, string position, string password)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Position = position;
        Password = password;
    }
}