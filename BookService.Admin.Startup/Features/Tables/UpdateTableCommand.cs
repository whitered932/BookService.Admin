using BookService.Admin.Startup.Converters;
using BookService.Admin.Startup.Features.Restaurant;
using BookService.Admin.Startup.Features.Restaurant.Models;
using BookService.Admin.Startup.Features.Tables.Errors;
using BookService.Admin.Startup.Features.Tables.Models;
using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Tables;

public class UpdateTableCommand : Command
{
    [FromBody] public UpdateTableDto Data { get; set; }
    [FromRoute] public long Id { get; set; }
}

public sealed class UpdateTableCommandHandler(ITableRepository tableRepository)
    : CommandHandler<UpdateTableCommand>
{
    public override async Task<Result> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var table = await tableRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (table == null)
        {
            return Error(TableNotFoundError.Instance);
        }
        var data = request.Data;
        var places = TableConverter.Convert(data.Places);
        table.Update(data.Title, data.ReserveAll, places);
        await tableRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}