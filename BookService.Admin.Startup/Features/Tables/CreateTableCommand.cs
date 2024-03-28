using BookService.Admin.Startup.Converters;
using BookService.Admin.Startup.Features.Tables.Models;
using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Tables;

public class CreateTableCommand : Command
{
    [FromBody] public CreateTableDto Data { get; set; }
}

public class CreateTableCommandHandler(ITableRepository tableRepository)
    : CommandHandler<CreateTableCommand>
{
    public override async Task<Result> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var data = request.Data;
        var places = TableConverter.Convert(data.Places);
        var table = new Table(data.Title, data.RestaurantId, data.ReserveAll, places);
        await tableRepository.AddAsync(table, cancellationToken);
        await tableRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}