using BookService.Admin.Startup.Converters;
using BookService.Admin.Startup.Features.Restaurant.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Restaurant;

public class GetRestaurantQuery : Query<RestaurantDto>
{
    [FromRoute] public long Id { get; set; }
}

public sealed class GetRestaurantQueryHandler(IRestaurantRepository restaurantRepository) : QueryHandler<GetRestaurantQuery, RestaurantDto>
{
    public override async Task<Result<RestaurantDto>> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (restaurant == null)
        {
            return Successful(new RestaurantDto());
        }
        return Successful(RestaurantConverter.Convert(restaurant));
    }
}