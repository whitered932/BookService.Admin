using BookService.Admin.Startup.Features.Reservations.Errors;
using BookService.Admin.Startup.Features.Reservations.Models;
using BookService.Admin.Startup.Services;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Ftsoft.Domain.Specification;

namespace BookService.Admin.Startup.Features.Client;

public class GetClientReservationQuery : Query<ReservationDto>
{
    public long Id { get; set; }
}

public sealed class GetClientReservationQueryHandler(
    IUserProvider userProvider,
    ITableRepository tableRepository,
    IReservationRepository reservationRepository,
    IClientRepository clientRepository,
    IRestaurantRepository restaurantRepository) : QueryHandler<GetClientReservationQuery, ReservationDto>
{
    public override async Task<Result<ReservationDto>> Handle(GetClientReservationQuery request, CancellationToken cancellationToken)
    {
        var clientEmail = userProvider.Email;
        var client = await clientRepository.SingleOrDefaultAsync(x => x.Email == clientEmail, cancellationToken);
        if (client is null)
        {
            return Error(ReservationNotFoundError.Instance);
        }
        var reservation = await reservationRepository.SingleOrDefaultAsync(x => x.Id == request.Id && x.ClientId == client.Id, cancellationToken);
        if (reservation is null)
        {
            return Error(ReservationNotFoundError.Instance);
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