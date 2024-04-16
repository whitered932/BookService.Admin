namespace BookService.Admin.Startup.Features.Restaurant.Models;

public class TimeSlotDto
{
    public long DateTime { get; set; }
    public long TableId { get; set; }
    public string TableName { get; set; }
    public int AvailablePlaces { get; set; }
}