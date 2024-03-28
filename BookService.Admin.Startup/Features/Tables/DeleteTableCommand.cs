using BookService.Admin.Startup.Features.Restaurant;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Tables;

public class DeleteTableCommand : Command
{
    [FromRoute] public long Id { get; set; }
}

public class DeleteTableCommandHandler(ITableRepository tableRepository) : CommandHandler<DeleteRestaurantCommand>
{
    public override async Task<Result> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var table = await tableRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await tableRepository.RemoveAsync(table!);
        await tableRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}