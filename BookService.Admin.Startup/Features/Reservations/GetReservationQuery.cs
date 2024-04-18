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

public sealed class GetReservationQueryHandler(IReservationRepository reservationRepository, IClientRepository clientRepository) : QueryHandler<GetReservationQuery, ReservationDto>
{
    public override async Task<Result<ReservationDto>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var client = await clientRepository.SingleOrDefaultAsync(x => reservation.ClientId == x.Id, cancellationToken);

        var result = new ReservationDto()
        {
            Comment = reservation.Comment,
            PersonsCount = reservation.PersonsCount,
            Date = ((DateTimeOffset)reservation.DateUtc).ToUnixTimeMilliseconds(),
            RestaurantId = reservation.RestaurantId,
            TableId = reservation.Table.TableId,
            ReservedPlacesCount = reservation.Table.PlaceIds.Count,
            Id = reservation.Id,
            ClientEmail = client?.Email,
            ClientPhone = client?.PhoneNumber,
            ClientName = client?.Name,
        };

        return Successful(result);
    }
}