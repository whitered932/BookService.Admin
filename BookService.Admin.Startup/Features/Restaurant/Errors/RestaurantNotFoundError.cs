using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Restaurant.Errors;

public class RestaurantNotFoundError : Error
{
    public override string Type => nameof(RestaurantNotFoundError);
    public static RestaurantNotFoundError Instance => new ();
}