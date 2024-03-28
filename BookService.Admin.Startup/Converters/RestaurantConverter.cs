using BookService.Domain.Models;
using BookService.Domain.Models.Enums;

namespace BookService.Admin.Startup.Features.Restaurant.Models;

public static class RestaurantConverter
{
    public static Domain.Models.Restaurant Convert(
        string title,
        string description,
        string startWorkTimeUtc,
        string endWorkTimeUtc,
        RestaurantContact contact,
        Enums.KitchenType kitchenType,
        double cost,
        int reservationThreshold,
        List<RestaurantPictureDto> pictures,
        List<RestaurantMenuItemDto> menuItemDtos)
    {
        var newContact = Convert(contact);
        var newKitchenType = Convert(kitchenType);
        var newPictures = Convert(pictures);
        var menu = Convert(menuItemDtos);
        var restaurant = new Domain.Models.Restaurant(
            title,
            description,
            startWorkTimeUtc,
            endWorkTimeUtc,
            newContact,
            newKitchenType,
            cost,
            reservationThreshold,
            newPictures,
            menu);
        return restaurant;
    }

    public static RestaurantDto Convert(Domain.Models.Restaurant restaurant)
    {
        var contact = Convert(restaurant.Contact);
        var kitchenType = Convert(restaurant.KitchenType);
        var pictures = Convert(restaurant.Pictures);
        var menuItems = Convert(restaurant.MenuItems);
        return new RestaurantDto()
        {
            Id = restaurant.Id,
            Title = restaurant.Title,
            Description = restaurant.Description,
            StartWorkTime = restaurant.StartWorkTimeLocal,
            EndWorkTime = restaurant.EndWorkTimeLocal,
            Contact = contact,
            KitchenType = kitchenType,
            Cost = restaurant.Cost,
            ReservationThreshold = restaurant.ReservationThreshold,
            Pictures = pictures,
            Menu = menuItems
        };
    }

    public static List<RestaurantDto> Convert(IEnumerable<Domain.Models.Restaurant> restaurants)
    {
        return restaurants.Select(Convert).ToList();
    }


    public static List<Domain.Models.RestaurantPicture> Convert(List<RestaurantPictureDto> pictures)
    {
        return pictures
            .Select(x => new Domain.Models.RestaurantPicture() { Title = x.Title, Url = x.Url })
            .ToList();
    }
    
    public static List<RestaurantMenuItemDto> Convert(List<RestaurantMenuItem> menu)
    {
        return menu
            .Select(x => new RestaurantMenuItemDto() { Title = x.Title, Cost = x.Cost, Weight = x.Weight})
            .ToList();
    }


    public static List<Domain.Models.RestaurantMenuItem> Convert(List<RestaurantMenuItemDto> menu)
    {
        return menu
            .Select(x => 
                new Domain.Models.RestaurantMenuItem()
                {
                    Title = x.Title,
                    Cost = x.Cost,
                    Weight = x.Weight
                })
            .ToList();
    }

    public static List<RestaurantPictureDto> Convert(List<Domain.Models.RestaurantPicture> pictures)
    {
        return pictures
            .Select(x => new RestaurantPictureDto() { Title = x.Title, Url = x.Url })
            .ToList();
    }

    public static Domain.Models.RestaurantContact Convert(RestaurantContact contact)
    {
        return new Domain.Models.RestaurantContact()
        {
            Address = contact.Address,
            Phone = contact.Phone,
            Email = contact.Email,
        };
    }

    public static RestaurantContact Convert(Domain.Models.RestaurantContact contact)
    {
        return new RestaurantContact()
        {
            Address = contact.Address,
            Phone = contact.Phone,
            Email = contact.Email,
        };
    }

    public static KitchenType Convert(Enums.KitchenType kitchenType)
    {
        return (KitchenType)(long)kitchenType;
    }

    public static Enums.KitchenType Convert(KitchenType kitchenType)
    {
        return (Enums.KitchenType)(long)kitchenType;
    }
}