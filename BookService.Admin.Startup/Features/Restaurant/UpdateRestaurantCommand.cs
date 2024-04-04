using BookService.Admin.Startup.Converters;
using BookService.Admin.Startup.Features.Restaurant.Enums;
using BookService.Admin.Startup.Features.Restaurant.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Restaurant;

public class UpdateRestaurantCommand : Command
{
    [FromBody] public UpdateRestaurantDto Data { get; set; }
    [FromRoute] public long Id { get; set; }
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

        var dto = request.Data;
        var newContact = RestaurantConverter.Convert(dto.Contact);
        var newKitchenType = RestaurantConverter.Convert(dto.KitchenType);
        var newPictures = RestaurantConverter.Convert(dto.Pictures);
        var menu = RestaurantConverter.Convert(dto.Menu);
        restaurant.Update(
            dto.Title,
            dto.Description,
            dto.StartWorkTime,
            dto.EndWorkTime,
            newContact,
            newKitchenType,
            dto.Cost,
            dto.ReservationThreshold,
            newPictures,
            menu);
        await restaurantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}