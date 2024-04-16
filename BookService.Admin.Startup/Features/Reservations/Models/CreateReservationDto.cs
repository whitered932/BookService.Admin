namespace BookService.Admin.Startup.Features.Reservations.Models;

public class CreateReservationDto
{
    public long Date { get; set; }
    public string Comment { get; set; }
    public long TableId { get; set; }
    public string ClientEmail { get; set; }
    public string ClientPhone { get; set; }
    public string ClientName { get; set; }
    public int PersonsCount { get; set; }
    public long RestaurantId { get; set; }
}