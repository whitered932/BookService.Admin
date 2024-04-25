using BookService.Admin.Startup.Features.Reservations.Errors;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Reservations;

public class SubmitReservationCommand : Command
{
    public long ReservationId { get; set; }
}

public sealed class SubmitReservationCommandHandler(IReservationRepository reservationRepository) : CommandHandler<SubmitReservationCommand>
{
    public override async Task<Result> Handle(SubmitReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.SingleOrDefaultAsync(x => x.Id == request.ReservationId, cancellationToken);
        if (reservation is null)
        {
            return Error(ReservationNotFoundError.Instance);
        }
        reservation.Accept();
        await reservationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}