using BookService.Admin.Startup.Features.Reservations.Models;
using BookService.Admin.Startup.Features.Tables.Errors;
using BookService.Domain.Models;
using BookService.Domain.Models.Enums;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Reservations;

public class CreateReservationCommand : Command
{
    [FromBody] public CreateReservationDto Data { get; set; }
}

public sealed class CreateReservationCommandHandler(
    ITableRepository tableRepository,
    IClientRepository clientRepository,
    IReservationRepository reservationRepository) : CommandHandler<CreateReservationCommand>
{
    public override async Task<Result> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var date = DateTime.SpecifyKind(DateTimeOffset.FromUnixTimeMilliseconds(request.Data.Date).DateTime,
            DateTimeKind.Utc);
        var table = await tableRepository.SingleOrDefaultAsync(x =>
            x.RestaurantId == request.Data.RestaurantId && x.Id == request.Data.TableId, cancellationToken);
        if (table is null)
        {
            return Error(TableNotFoundError.Instance);
        }

        var client =
            await clientRepository.FirstOrDefaultAsync(x => x.Email == request.Data.ClientEmail, cancellationToken);
        if (client is null)
        {
            client = new Domain.Models.Client(request.Data.ClientName, request.Data.ClientPhone,
                request.Data.ClientEmail);
            await clientRepository.AddAsync(client, cancellationToken);
            await clientRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        if (request.Data.PersonsCount > table.Places.Count)
        {
            return Error(TableNotFoundError.Instance);
        }

        var existsReservations =
            await reservationRepository.ListAsync(
                x =>
                    x.Table.TableId == table.Id &&
                    x.DateUtc == date &&
                    (x.Status == ReservationStatus.Requested || x.Status == ReservationStatus.AcceptedByManager),
                cancellationToken);
        if (table.ReserveAll && existsReservations.Any())
        {
            return Error(TableNotFoundError.Instance);
        }


        List<long> placeNumbers;
        if (table.ReserveAll)
        {
            placeNumbers = table.Places.Select(x => x.Number).ToList();
        }
        else
        {
            var occupiedPlaces = existsReservations.SelectMany(x => x.Table.PlaceIds);
            placeNumbers = table.Places.Where(x => !occupiedPlaces.Contains(x.Number)).Take(request.Data.PersonsCount)
                .Select(x => x.Number).ToList();
        }

        var tableInfo = new TableInfo(request.Data.TableId, placeNumbers);
        var reservation = new Reservation(request.Data.Comment, date, ReservationStatus.Requested,
            tableInfo, request.Data.RestaurantId, client.Id, request.Data.PersonsCount);
        await reservationRepository.AddAsync(reservation, cancellationToken);
        await reservationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}