using BookService.Admin.Startup.Features.Reservations.Errors;
using BookService.Admin.Startup.Features.Reservations.Models;
using BookService.Admin.Startup.Services;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Client;

public class GetClientReservationsQuery : Query<IReadOnlyList<ReservationDto>>
{
}

public sealed class GetClientReservationsQueryHandler(
    IUserProvider userProvider,
    ITableRepository tableRepository,
    IReservationRepository reservationRepository,
    IClientRepository clientRepository,
    IRestaurantRepository restaurantRepository)
    : QueryHandler<GetClientReservationsQuery, IReadOnlyList<ReservationDto>>
{
    public override async Task<Result<IReadOnlyList<ReservationDto>>> Handle(GetClientReservationsQuery request,
        CancellationToken cancellationToken)
    {
        var clientEmail = userProvider.Email;
        var client = await clientRepository.SingleOrDefaultAsync(x => x.Email == clientEmail, cancellationToken);
        if (client is null)
        {
            return Error(ReservationNotFoundError.Instance);
        }


        var reservations = await reservationRepository.ListAsync(x => x.ClientId == client.Id, cancellationToken);
        var tables = await tableRepository.ListAsync(
            x => reservations.Select(reservation => reservation.Table.TableId).Contains(x.Id),
            cancellationToken);
        var restaurants = await restaurantRepository.ListAsync(
            x => reservations.Select(reservation => reservation.RestaurantId).Contains(x.Id), cancellationToken);
        var restDict = restaurants.ToDictionary(x => x.Id);
        var tablesDict = tables.ToDictionary(x => x.Id);

        var result = reservations.Select(x => new ReservationDto()
        {
            Comment = x.Comment,
            PersonsCount = x.PersonsCount,
            Date = ((DateTimeOffset)x.DateUtc).ToUnixTimeMilliseconds(),
            RestaurantId = x.RestaurantId,
            ReservedPlacesCount = x.Table.PlaceIds.Count,
            TableId = x.Table.TableId,
            Id = x.Id,
            ClientEmail = client.Email,
            ClientPhone = client.PhoneNumber,
            ClientName = client.Name,
            Status = x.Status,
            TableName = tablesDict[x.Table.TableId].Title,
            RestaurantName = restDict[x.RestaurantId].Title,
        }).ToList();
        return Successful(result);
    }
}