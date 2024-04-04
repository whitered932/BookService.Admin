using BookService.Admin.Startup.Converters;
using BookService.Admin.Startup.Features.Restaurant.Enums;
using BookService.Admin.Startup.Features.Restaurant.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Restaurant;

public class CreateRestaurantCommand : Command
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

public class CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository)
    : CommandHandler<CreateRestaurantCommand>
{
    public override async Task<Result> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var contact = RestaurantConverter.Convert(request.Contact);
        var newKitchenType = RestaurantConverter.Convert(request.KitchenType);
        var newPictures = RestaurantConverter.Convert(request.Pictures);
        var menu = RestaurantConverter.Convert(request.Menu);
        var restaurant = new Domain.Models.Restaurant(
            request.Title,
            request.Description,
            request.StartWorkTime,
            request.EndWorkTime,
            contact,
            newKitchenType,
            request.Cost,
            request.ReservationThreshold,
            newPictures,
            menu);
        await restaurantRepository.AddAsync(restaurant, cancellationToken);
        await restaurantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}