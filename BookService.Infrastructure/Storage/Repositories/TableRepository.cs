using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace BookService.Infrastructure.Storage.Repositories;

public class TableRepository : EFRepository<Table, BookServiceDbContext>, ITableRepository
{
    public TableRepository(BookServiceDbContext context) : base(context)
    {
    }
}