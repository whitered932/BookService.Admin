using BookService.Admin.Startup.Features.Restaurant.Models;
using BookService.Domain.Models.Enums;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Reservations;

public class GetTimeSlotsQuery : Query<IReadOnlyList<TimeSlotDto>>
{
    public long RestaurantId { get; set; }
    public long DateTime { get; set; }
}

public sealed class GetTimeSlotsQueryHandler(
    IReservationRepository reservationRepository,
    IRestaurantRepository restaurantRepository,
    ITableRepository tableRepository) : QueryHandler<GetTimeSlotsQuery, IReadOnlyList<TimeSlotDto>>
{
    public override async Task<Result<IReadOnlyList<TimeSlotDto>>> Handle(GetTimeSlotsQuery request,
        CancellationToken cancellationToken)
    {
        var restaurant =
            await restaurantRepository.SingleOrDefaultAsync(x => x.Id == request.RestaurantId, cancellationToken);

        var dateTime = DateTime.SpecifyKind(DateTimeOffset.FromUnixTimeMilliseconds(request.DateTime).DateTime,
            DateTimeKind.Utc).Date;
        var dateOnly = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

        var startData = restaurant.StartWorkTimeLocal.Split(":").Select(int.Parse).ToArray();
        var startTime = new TimeOnly(startData[0], startData[1]);

        var endData = restaurant.EndWorkTimeLocal.Split(":").Select(int.Parse).ToArray();
        var endTime = new TimeOnly(endData[0], endData[1]);


        var startDateTime = new DateTime(dateOnly, startTime, DateTimeKind.Utc);
        var endDateTime = new DateTime(dateOnly, endTime, DateTimeKind.Utc);
        if (endDateTime <= startDateTime)
        {
            endDateTime = endDateTime.AddDays(1);
        }

        var reservations =
            await reservationRepository.ListAsync(
                x =>
                    x.RestaurantId == request.RestaurantId &&
                    startDateTime <= x.DateUtc &&
                    x.DateUtc <= endDateTime &&
                    (x.Status == ReservationStatus.AcceptedByManager || x.Status == ReservationStatus.Requested),
                cancellationToken);
        var tables = await tableRepository.ListAsync(x => x.RestaurantId == restaurant.Id, cancellationToken);
        var timeSlots = new List<TimeSlotDto>();
        foreach (var table in tables)
        { 
            var currentTime = startDateTime;
            while (currentTime < endDateTime)
            {
                var date = ((DateTimeOffset)currentTime).ToUnixTimeMilliseconds();
                var timeSlot = new TimeSlotDto()
                {
                    DateTime = date,
                    TableId = table.Id,
                    TableName = table.Title,
                    AvailablePlaces = table.Places.Count,
                };
                var nextTime = currentTime.AddMinutes(restaurant.ReservationThreshold);
                var time = currentTime;
                var reservationsOnTime = reservations.Where(x =>
                    x.DateUtc >= time &&
                    x.DateUtc < nextTime &&
                    x.Table.TableId == table.Id &&
                    x.Status is ReservationStatus.Requested or ReservationStatus.AcceptedByManager).ToList();
                if (reservationsOnTime.Count != 0)
                {
                    if (table.ReserveAll)
                    {
                        timeSlot.AvailablePlaces = 0;
                    }
                    else
                    {
                        var reservedCount = reservationsOnTime.Select(x => x.Table.PlaceIds).Count();
                        timeSlot.AvailablePlaces = table.Places.Count - reservedCount;
                    }
                }
                timeSlots.Add(timeSlot);
                currentTime = nextTime;
            }
        }

        return Successful(timeSlots.OrderBy(x => x.AvailablePlaces).ToList());
    }
}