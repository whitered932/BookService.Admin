using BookService.Domain.Models.Enums;

namespace BookService.Domain.Models;

public class Restaurant : BaseModel
{
    private Restaurant()
    {
    }

    public Restaurant(
        string title,
        string description,
        string startWorkTimeLocal,
        string endWorkTimeLocal,
        RestaurantContact contact,
        KitchenType kitchenType,
        double cost,
        int reservationThreshold,
        List<RestaurantPicture> pictures, List<RestaurantMenuItem> menu)
    {
        Title = title;
        Description = description;
        StartWorkTimeLocal = startWorkTimeLocal;
        EndWorkTimeLocal = endWorkTimeLocal;
        Contact = contact;
        KitchenType = kitchenType;
        Cost = cost;
        ReservationThreshold = reservationThreshold;
        Pictures = pictures;
        MenuItems = menu;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string StartWorkTimeLocal { get; private set; }
    public string EndWorkTimeLocal { get; private set; }
    public RestaurantContact Contact { get; private set; }
    public KitchenType KitchenType { get; private set; }
    public double Cost { get; private set; }
    public int ReservationThreshold { get; private set; }
    public List<RestaurantPicture> Pictures { get; private set; } = [];
    public List<RestaurantMenuItem> MenuItems { get; private set; } = [];

    public void Update(
        string title,
        string description,
        string startWorkTimeUtc,
        string endWorkTimeUtc,
        RestaurantContact contact,
        KitchenType kitchenType,
        double cost,
        int reservationThreshold,
        List<RestaurantPicture> pictures,
        List<RestaurantMenuItem> menuItems)
    {
        Title = title;
        Description = description;
        StartWorkTimeLocal = startWorkTimeUtc;
        EndWorkTimeLocal = endWorkTimeUtc;
        Contact = contact;
        KitchenType = kitchenType;
        Cost = cost;
        ReservationThreshold = reservationThreshold;
        Pictures = pictures;
        MenuItems = menuItems;
    }
}

public class RestaurantContact
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}

public class RestaurantPicture
{
    public string Url { get;  set; }
    public string Title { get;  set; }
}

public class RestaurantMenuItem
{
    public string Title { get; set; }
    public string Weight { get; set; }
    public double Cost { get; set; }
}