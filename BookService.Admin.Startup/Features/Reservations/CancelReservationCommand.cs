using BookService.Admin.Startup.Features.Reservations.Errors;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Reservations;

public class CancelReservationCommand  : Command
{
    public long ReservationId { get; set; }
}



public sealed class CancelReservationCommandHandler(IReservationRepository reservationRepository) : CommandHandler<CancelReservationCommand>
{
    public override async Task<Result> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.SingleOrDefaultAsync(x => x.Id == request.ReservationId, cancellationToken);
        if (reservation is null)
        {
            return Error(ReservationNotFoundError.Instance);
        }

        reservation.DeclineByManager();
        await reservationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}