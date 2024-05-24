using BookService.Admin.Startup.Features.Reservations.Models;
using BookService.Admin.Startup.Features.Restaurant.Errors;
using BookService.Admin.Startup.Services;
using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Reservations;

public class GetReservationsQuery : Query<IReadOnlyList<ReservationDto>>
{
}

public sealed class GetReservationsQueryHandler(IEmployeeRepository employeeRepository, IUserProvider userProvider,
    ITableRepository tableRepository, IRestaurantRepository restaurantRepository,
    IReservationRepository reservationRepository,
    IClientRepository clientRepository) : QueryHandler<GetReservationsQuery, IReadOnlyList<ReservationDto>>
{
    private Domain.Models.Restaurant? _restaurant;

    protected override async Task<Ftsoft.Common.Result.IResult> CanHandle(GetReservationsQuery request,
        CancellationToken cancellationToken)
    {
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
        if (restaurant is null)
        {
            return Error(RestaurantNotFoundError.Instance);
        }

        _restaurant = restaurant;
        return Successful();
    }


    public override async Task<Result<IReadOnlyList<ReservationDto>>> Handle(GetReservationsQuery request,
        CancellationToken cancellationToken)
    {
        var reservations =
            await reservationRepository.ListAsync(x => _restaurant == null || x.RestaurantId == _restaurant.Id,
                cancellationToken);
        var clientsById = await GetClients(reservations, cancellationToken);
        var tables = await tableRepository.ListAsync(x => reservations.Select(x => x.Table.TableId).Contains(x.Id),
            cancellationToken);
        var tablesDict = tables.ToDictionary(x => x.Id);

        var restaurants =
            await restaurantRepository.ListAsync(
                x => reservations.Select(reservation => reservation.RestaurantId).Contains(x.Id),
                cancellationToken);
        var restDict = restaurants.ToDictionary(x => x.Id);

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

    private async Task<Dictionary<long, Domain.Models.Client>> GetClients(IReadOnlyList<Reservation> reservations,
        CancellationToken cancellationToken)
    {
        var clientIds = reservations.Select(x => x.ClientId).ToList();
        var clients = await clientRepository.ListAsync(x => clientIds.Contains(x.Id), cancellationToken);
        var clientsById = clients.ToDictionary(x => x.Id);
        return clientsById;
    }
}