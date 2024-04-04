using BookService.Domain.Models;
using Ftsoft.Storage;

namespace BookService.Domain.Repositories;

public interface IAuthorizationTokenRepository : IRepository<AuthorizationToken>;