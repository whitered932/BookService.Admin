namespace BookService.Admin.Startup.Features.Employees.Models;

public class UpdateEmployeeDto
{
    public string Position { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}