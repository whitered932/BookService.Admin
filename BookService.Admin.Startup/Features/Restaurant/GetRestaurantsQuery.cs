using BookService.Admin.Startup.Converters;
using BookService.Admin.Startup.Features.Restaurant.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Restaurant;

public class GetRestaurantsQuery : Query<IReadOnlyList<RestaurantDto>>
{
    
}

public sealed class GetRestaurantsQueryHandler(IRestaurantRepository restaurantRepository) : QueryHandler<GetRestaurantsQuery, IReadOnlyList<RestaurantDto>>
{
    public override async Task<Result<IReadOnlyList<RestaurantDto>>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var restaurants = await restaurantRepository.ListAsync(cancellationToken);
        return Successful(RestaurantConverter.Convert(restaurants));
    }
}