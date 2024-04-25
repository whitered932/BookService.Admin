using BookService.Admin.Startup.Features.Reservations.Errors;
using BookService.Admin.Startup.Features.Reservations.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Reservations;

public class GetReservationQuery : Query<ReservationDto>
{
    [FromRoute] public long Id { get; set; }
}

public sealed class GetReservationQueryHandler(ITableRepository tableRepository, IReservationRepository reservationRepository, IClientRepository clientRepository, IRestaurantRepository restaurantRepository) : QueryHandler<GetReservationQuery, ReservationDto>
{
    public override async Task<Result<ReservationDto>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reservation is null)
        {
            return Error(ReservationNotFoundError.Instance);
        }
        var client = await clientRepository.SingleOrDefaultAsync(x => reservation.ClientId == x.Id, cancellationToken);
        if (client is null)
        {
            return Error(ClientNotFoundError.Instance);
        }

        var restaurant = await restaurantRepository.SingleOrDefaultAsync(x => reservation.RestaurantId == x.Id, cancellationToken);
        var table = await tableRepository.SingleOrDefaultAsync(x => reservation.Table.TableId == x.Id, cancellationToken);
        var result = new ReservationDto()
        {
            Comment = reservation.Comment,
            PersonsCount = reservation.PersonsCount,
            Date = ((DateTimeOffset)reservation.DateUtc).ToUnixTimeMilliseconds(),
            RestaurantId = reservation.RestaurantId,
            TableId = reservation.Table.TableId,
            TableName = table?.Title ?? "Неизвестно",
            ReservedPlacesCount = reservation.Table.PlaceIds.Count,
            Id = reservation.Id,
            ClientEmail = client.Email,
            ClientPhone = client.PhoneNumber,
            ClientName = client.Name,
            Status = reservation.Status,
            RestaurantName = restaurant?.Title ?? "Неизвестно"
        };

        return Successful(result);
    }
}