using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace BookService.Infrastructure.Storage.Repositories;

public class AuthorizationTokenRepository : EFRepository<AuthorizationToken, BookServiceDbContext>, IAuthorizationTokenRepository
{
    public AuthorizationTokenRepository(BookServiceDbContext context) : base(context)
    {
    }
}