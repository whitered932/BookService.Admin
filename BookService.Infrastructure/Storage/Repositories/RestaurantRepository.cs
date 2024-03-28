using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace BookService.Infrastructure.Storage.Repositories;

public class RestaurantRepository : EFRepository<Restaurant, BookServiceDbContext>, IRestaurantRepository
{
    public RestaurantRepository(BookServiceDbContext context) : base(context)
    {
    }
}