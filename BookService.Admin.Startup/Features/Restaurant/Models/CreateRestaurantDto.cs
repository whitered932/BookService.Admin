using BookService.Admin.Startup.Features.Restaurant.Enums;

namespace BookService.Admin.Startup.Features.Restaurant.Models;

public class CreateRestaurantDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string StartWorkTime { get; set; }
    public string EndWorkTime { get; set; }
    public RestaurantContact Contact { get; set; }
    public KitchenType KitchenType { get; set; }
    public double Cost { get; set; }
    public int ReservationThreshold { get; set; }
    public List<RestaurantPictureDto> Pictures { get; set; } = [];
    public List<RestaurantMenuItemDto> Menu { get; set; } = [];
}