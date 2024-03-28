using BookService.Admin.Startup.Converters;
using BookService.Admin.Startup.Features.Restaurant;
using BookService.Admin.Startup.Features.Tables.Errors;
using BookService.Admin.Startup.Features.Tables.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Tables;

public class GetTableQuery : Query<TableDto>
{
    [FromRoute] public long Id { get; set; }
}

public sealed class GetTableQueryHandler(ITableRepository tableRepository) : QueryHandler<GetTableQuery, TableDto>
{
    public override async Task<Result<TableDto>> Handle(GetTableQuery request, CancellationToken cancellationToken)
    {
        var table = await tableRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (table == null)
        {
            return Error(TableNotFoundError.Instance);
        }

        var tableDto = TableConverter.Convert(table);
        return Successful(tableDto);
    }
}