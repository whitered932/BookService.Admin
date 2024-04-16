using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace BookService.Infrastructure.Storage.Repositories;

public class ReservationRepository : EFRepository<Reservation, BookServiceDbContext>, IReservationRepository
{
    public ReservationRepository(BookServiceDbContext context) : base(context)
    {
    }
}