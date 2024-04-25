using BookService.Admin.Startup.Features.Reservations.Errors;
using BookService.Admin.Startup.Features.Reservations.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Reservations;

public class UpdateReservationCommand : Command
{
    [FromRoute] public long Id { get; set; }
    [FromBody] public CreateReservationDto Data { get; set; }
}

public sealed class UpdateReservationCommandHandler(IReservationRepository reservationRepository) : CommandHandler<UpdateReservationCommand>
{
    public override async Task<Result> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reservation is null)
        {
            return Error(ReservationNotFoundError.Instance);
        }
        reservation.Update(request.Data.Comment);
        await reservationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}