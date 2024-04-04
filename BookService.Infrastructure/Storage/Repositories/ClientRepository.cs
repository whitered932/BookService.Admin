using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace BookService.Infrastructure.Storage.Repositories;

public class ClientRepository : EFRepository<Client, BookServiceDbContext>, IClientRepository
{
    public ClientRepository(BookServiceDbContext context) : base(context)
    {
    }
}