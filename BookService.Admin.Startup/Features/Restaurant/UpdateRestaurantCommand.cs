using BookService.Admin.Startup.Features.Restaurant.Enums;
using BookService.Admin.Startup.Features.Restaurant.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Restaurant;

public class UpdateRestaurantCommand : Command
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
    public long Id { get; set; }
}

public sealed class UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository)
    : CommandHandler<UpdateRestaurantCommand>
{
    public override async Task<Result> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (restaurant == null)
        {
            return Successful();
        }

        var newContact = Converter.Convert(request.Contact);
        var newKitchenType = Converter.Convert(request.KitchenType);
        var newPictures = Converter.Convert(request.Pictures);
        var menu = Converter.Convert(request.Menu);
        restaurant.Update(
            request.Title,
            request.Description,
            request.StartWorkTime,
            request.EndWorkTime,
            newContact,
            newKitchenType,
            request.Cost,
            request.ReservationThreshold,
            newPictures,
            menu);
        await restaurantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}