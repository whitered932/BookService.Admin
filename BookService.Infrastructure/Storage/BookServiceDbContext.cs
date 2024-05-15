using BookService.Domain.Models;
using Ftsoft.Storage;
using Microsoft.EntityFrameworkCore;

namespace BookService.Infrastructure.Storage;

public class BookServiceDbContext : DbContext, IUnitOfWork
{
    public BookServiceDbContext()
    {
    }

    public BookServiceDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=booking-service;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookServiceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Client> Clients  { get; set; }
    public DbSet<Employee> Employees  { get; set; }
    public DbSet<AuthorizationToken> AuthorizationTokens  { get; set; }
}