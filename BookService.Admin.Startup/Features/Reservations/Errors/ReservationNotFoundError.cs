using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Reservations.Errors;

public class ReservationNotFoundError : Error
{
    public override string Type => nameof(ReservationNotFoundError);
    public static ReservationNotFoundError Instance => new();
}