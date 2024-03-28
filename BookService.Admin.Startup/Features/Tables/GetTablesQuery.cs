using System.ComponentModel.DataAnnotations;
using BookService.Admin.Startup.Converters;
using BookService.Admin.Startup.Features.Tables.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Tables;

public class GetTablesQuery : Query<IReadOnlyList<TableDto>>
{
    [Required]
    [FromQuery(Name = "restaurantId")]
    public long RestaurantId { get; set; }
}

public sealed class GetTablesQueryHandler(ITableRepository tableRepository)
    : QueryHandler<GetTablesQuery, IReadOnlyList<TableDto>>
{
    public override async Task<Result<IReadOnlyList<TableDto>>> Handle(GetTablesQuery request,
        CancellationToken cancellationToken)
    {
        var tables = await tableRepository.ListAsync(x => x.RestaurantId == request.RestaurantId, cancellationToken);
        var tableDtos = TableConverter.Convert(tables);
        return Successful(tableDtos);
    }
}