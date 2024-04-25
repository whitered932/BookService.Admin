using BookService.Admin.Startup.Features.Reservations.Errors;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Reservations;

public class DeleteReservationCommand : Command
{
    [FromRoute] public long Id { get; set; }
}

public sealed class DeleteReservationCommandHandler(IReservationRepository reservationRepository) : CommandHandler<DeleteReservationCommand>
{
    public override async Task<Result> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reservation is null)
        {
            return Error(ReservationNotFoundError.Instance);
        }
        await reservationRepository.RemoveAsync(reservation);
        await reservationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}