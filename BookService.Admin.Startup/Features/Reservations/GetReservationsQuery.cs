using System.Diagnostics.CodeAnalysis;
using BookService.Admin.Startup.Features.Reservations.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Ftsoft.Domain.Specification;

namespace BookService.Admin.Startup.Features.Reservations;

public class GetReservationsQuery : Query<IReadOnlyList<ReservationDto>>
{
    
}

public sealed class GetReservationsQueryHandler(ITableRepository tableRepository, IRestaurantRepository restaurantRepository, IReservationRepository reservationRepository, IClientRepository clientRepository) : QueryHandler<GetReservationsQuery, IReadOnlyList<ReservationDto>>
{
    public override async Task<Result<IReadOnlyList<ReservationDto>>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await reservationRepository.ListAsync(cancellationToken);
        var clientIds = reservations.Select(x => x.ClientId).ToList();
        var clients = await clientRepository.ListAsync(x => clientIds.Contains(x.Id), cancellationToken);
        var clientsById = clients.ToDictionary(x => x.Id);
        var tables = await tableRepository.ListAsync(x => reservations.Select(x => x.Table.TableId).Contains(x.Id), cancellationToken);
        var restaurants = await restaurantRepository.ListAsync(x => reservations.Select(x => x.RestaurantId).Contains(x.Id), cancellationToken);
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
            ClientEmail = clientsById[(long)x.ClientId].Email,
            ClientPhone = clientsById[(long)x.ClientId].PhoneNumber,
            ClientName = clientsById[(long)x.ClientId].Name,
            Status = x.Status,
            TableName = tablesDict[x.Table.TableId].Title,
            RestaurantName = restDict[x.RestaurantId].Title,
        }).ToList();
        return Successful(result);
    }
}