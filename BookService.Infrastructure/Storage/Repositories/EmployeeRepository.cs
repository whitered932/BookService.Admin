using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace BookService.Infrastructure.Storage.Repositories;

public class EmployeeRepository : EFRepository<Employee, BookServiceDbContext>, IEmployeeRepository
{
    public EmployeeRepository(BookServiceDbContext context) : base(context)
    {
    }
}