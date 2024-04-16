using BookService.Domain.Models.Enums;

namespace BookService.Domain.Models;

public class Reservation : BaseModel
{
    private Reservation()
    {
    }
    
    public Reservation(string comment, DateTime dateUtc, ReservationStatus status, TableInfo table,
        long restaurantId, long? clientId, int personsCount)
    {
        Comment = comment;
        DateUtc = dateUtc;
        Status = status;
        Table = table;
        RestaurantId = restaurantId;
        ClientId = clientId;
        PersonsCount = personsCount;
    }

    public string Comment { get; private set; }
    public DateTime DateUtc { get; private set; }
    public ReservationStatus Status { get; private set; }
    public int PersonsCount { get; private set; }
    public TableInfo Table { get; private set; }

    public long RestaurantId { get; private set; }
    public long? ClientId { get; private set; }
}

public class TableInfo
{
    public TableInfo(long tableId, List<long> placeIds)
    {
        TableId = tableId;
        PlaceIds = placeIds;
    }

    public long TableId { get; private set; }
    public List<long> PlaceIds { get; private set; }
}