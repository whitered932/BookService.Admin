using BookService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookService.Infrastructure.Storage.TypeConfigurations;

public class TableTypeConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.ReserveAll).IsRequired();
        builder.Property(x => x.RestaurantId).IsRequired();

        builder.OwnsMany(x => x.Places);
    }
}