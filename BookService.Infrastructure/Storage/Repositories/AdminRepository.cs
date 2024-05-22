using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace BookService.Infrastructure.Storage.Repositories;

public class AdminRepository : EFRepository<Admin, BookServiceDbContext>, IAdminRepository
{
    public AdminRepository(BookServiceDbContext context) : base(context)
    {
    }
}