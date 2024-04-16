namespace BookService.Domain.Models;

public class TimeSlot : BaseModel
{
    public long RestaurantId { get; private set; }
    public long CustomerId { get; private set; }
    public bool IsReserved { get; private set; }

    public class TimeSlotCustomerInfo
    {
        
    }
}