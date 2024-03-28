namespace BookService.Domain.Models;

public class Table : BaseModel
{
    private Table() {}
    
    public Table(string title, long restaurantId, bool reserveAll, List<TablePlace> places)
    {
        Title = title;
        RestaurantId = restaurantId;
        ReserveAll = reserveAll;
        Places = places;
    }
    

    public string Title { get; private set; }
    public long RestaurantId { get; private set; }
    public bool ReserveAll { get; private set; }

    public void Update(string title, bool reserveAll, List<TablePlace> places)
    {
        Title = title;
        ReserveAll = reserveAll;
        Places = places;
    }
    
    public List<TablePlace> Places { get; private set; }
}


public class TablePlace
{
    private TablePlace()
    {
    }

    public TablePlace(string title, long number)
    {
        Title = title;
        Number = number;
    }
    public string Title { get; private set; }
    public long Number { get; private set; }
}