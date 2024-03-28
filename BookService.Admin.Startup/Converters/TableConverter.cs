using BookService.Admin.Startup.Features.Tables.Models;
using BookService.Domain.Models;

namespace BookService.Admin.Startup.Converters;

public static class TableConverter
{
    public static TableDto Convert(Table table)
    {
        return new TableDto()
        {
            Id = table.Id,
            Title = table.Title,
            ReserveAll = table.ReserveAll,
            RestaurantId = table.RestaurantId,
            Places = Convert(table.Places)
        };
    }

    public static List<TableDto> Convert(IEnumerable<Table> tables)
    {
        return tables.Select(Convert).ToList();
    }

    public static List<TablePlace> Convert(IEnumerable<TablePlaceDto> places)
    {
        return places.Select(x => new TablePlace(x.Title, x.Number)).ToList();
    }

    public static List<TablePlaceDto> Convert(IEnumerable<TablePlace> places)
    {
        return places.Select(x => new TablePlaceDto { Title = x.Title, Number = x.Number }).ToList();
    }
}