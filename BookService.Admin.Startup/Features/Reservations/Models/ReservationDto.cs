namespace BookService.Admin.Startup.Features.Reservations.Models;

public class ReservationDto : CreateReservationDto
{
    public long Id { get; set; }
    public long ReservedPlacesCount { get; set; }
}