using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Restaurant;

public class DeleteRestaurantCommand : Command
{
    [FromRoute] public long Id { get; set; }
}

public class DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository) : CommandHandler<DeleteRestaurantCommand>
{
    public override async Task<Result> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await restaurantRepository.RemoveAsync(restaurant!);
        await restaurantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}