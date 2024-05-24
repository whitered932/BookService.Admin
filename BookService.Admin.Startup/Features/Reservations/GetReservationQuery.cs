using BookService.Admin.Startup.Features.Reservations.Errors;
using BookService.Admin.Startup.Features.Reservations.Models;
using BookService.Admin.Startup.Features.Restaurant.Errors;
using BookService.Admin.Startup.Services;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace BookService.Admin.Startup.Features.Reservations;

public class GetReservationQuery : Query<ReservationDto>
{
    [FromRoute] public long Id { get; set; }
}

public sealed class GetReservationQueryHandler(
    IEmployeeRepository employeeRepository,
    IUserProvider userProvider,
    ITableRepository tableRepository,
    IReservationRepository reservationRepository,
    IClientRepository clientRepository,
    IRestaurantRepository restaurantRepository) : QueryHandler<GetReservationQuery, ReservationDto>
{
    private Domain.Models.Reservation _reservation;

    protected override async Task<Ftsoft.Common.Result.IResult> CanHandle(GetReservationQuery request,
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


    public override async Task<Result<ReservationDto>> Handle(GetReservationQuery request,
        CancellationToken cancellationToken)
    {
        var client = await clientRepository.SingleOrDefaultAsync(x => _reservation.ClientId == x.Id, cancellationToken);
        if (client is null)
        {
            return Error(ClientNotFoundError.Instance);
        }

        var restaurant =
            await restaurantRepository.SingleOrDefaultAsync(x => _reservation.RestaurantId == x.Id, cancellationToken);
        var table = await tableRepository.SingleOrDefaultAsync(x => _reservation.Table.TableId == x.Id,
            cancellationToken);
        var result = new ReservationDto()
        {
            Comment = _reservation.Comment,
            PersonsCount = _reservation.PersonsCount,
            Date = ((DateTimeOffset)_reservation.DateUtc).ToUnixTimeMilliseconds(),
            RestaurantId = _reservation.RestaurantId,
            TableId = _reservation.Table.TableId,
            TableName = table?.Title ?? "Неизвестно",
            ReservedPlacesCount = _reservation.Table.PlaceIds.Count,
            Id = _reservation.Id,
            ClientEmail = client.Email,
            ClientPhone = client.PhoneNumber,
            ClientName = client.Name,
            Status = _reservation.Status,
            RestaurantName = restaurant?.Title ?? "Неизвестно"
        };

        return Successful(result);
    }
}