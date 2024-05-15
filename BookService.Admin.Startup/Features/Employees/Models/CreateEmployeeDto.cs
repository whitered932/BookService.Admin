namespace BookService.Admin.Startup.Features.Employees.Models;

public class CreateEmployeeDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public long RestaurantId { get; set; }
    public string Position { get; set; }
    public string Password { get; set; }
}