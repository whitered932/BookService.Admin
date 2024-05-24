using BookService.Admin.Startup.Features.Reservations.Errors;
using BookService.Admin.Startup.Features.Restaurant.Errors;
using BookService.Admin.Startup.Services;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Reservations;

public class DeleteReservationCommand : Command
{
    [FromRoute] public long Id { get; set; }
}

public sealed class DeleteReservationCommandHandler(
    IEmployeeRepository employeeRepository,
    IUserProvider userProvider,
    IRestaurantRepository restaurantRepository,
    IReservationRepository reservationRepository
    ) : CommandHandler<DeleteReservationCommand>
{
    private Domain.Models.Reservation _reservation;

    protected override async Task<Ftsoft.Common.Result.IResult> CanHandle(DeleteReservationCommand request,
        CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reservation is null)
        {
            return Error(ReservationNotFoundError.Instance);
        }
        _reservation = reservation;

        if (userProvider.Role == "Admin")
        {
            return Successful();
        }

        var id = long.Parse(userProvider.Id!);
        var employee = await employeeRepository.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (employee is null)
        {
            return Error(RestaurantNotFoundError.Instance);
        }

        var restaurant =
            await restaurantRepository.SingleOrDefaultAsync(x => x.Id == employee.RestaurantId,
                cancellationToken);
        if (restaurant is null || request.Id != restaurant.Id)
        {
            return Error(RestaurantNotFoundError.Instance);
        }
        
        return reservation.RestaurantId == restaurant.Id ? Successful() : Error(RestaurantNotFoundError.Instance);
    }
    
    public override async Task<Result> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        await reservationRepository.RemoveAsync(_reservation);
        await reservationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}