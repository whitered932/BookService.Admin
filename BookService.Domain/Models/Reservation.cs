using BookService.Domain.Models.Enums;

namespace BookService.Domain.Models;

public class Reservation : BaseModel
{
    public Reservation(string comment, DateTime dateUtc, ReservationStatus status, List<TableInfo> tables,
        long restaurantId, long? clientId)
    {
        Comment = comment;
        DateUtc = dateUtc;
        Status = status;
        Tables = tables;
        RestaurantId = restaurantId;
        ClientId = clientId;
    }

    public string Comment { get; private set; }
    public DateTime DateUtc { get; private set; }
    public ReservationStatus Status { get; private set; }
    public List<TableInfo> Tables { get; private set; }

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