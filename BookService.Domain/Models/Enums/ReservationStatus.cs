namespace BookService.Domain.Models.Enums;

public enum ReservationStatus
{
    Requested = 0,
    AcceptedByManager = 1,
    DeclinedByClient = 2,
    DeclinedByManager = 3
}