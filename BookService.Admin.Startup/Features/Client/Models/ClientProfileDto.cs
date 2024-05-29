namespace BookService.Admin.Startup.Features.Client.Models;

public class ClientProfileDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public long? RestaurantId { get; set; }
    public string Role { get; set; }
}