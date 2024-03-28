namespace BookService.Admin.Startup.Features.Tables.Models;

public class CreateTableDto
{
    public string Title { get; set; }
    public bool ReserveAll { get; set; }
    public long RestaurantId { get; set; }
    public List<TablePlaceDto> Places { get; set; }
}