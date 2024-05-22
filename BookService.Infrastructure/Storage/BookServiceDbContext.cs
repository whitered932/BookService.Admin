using BookService.Domain.Models;
using BookService.Infrastructure.Storage.Repositories;
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
        optionsBuilder.UseNpgsql("Host=rc1b-ceb7bmifqoko9cxh.mdb.yandexcloud.net;Port=6432;Database=db1;Username=user1;Password=123123123");
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
    public DbSet<Admin> Admins  { get; set; }
}