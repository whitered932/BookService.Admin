using BookService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookService.Infrastructure.Storage.TypeConfigurations;

public class RestaurantTypeConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.Property(x => x.ReservationThreshold);
        builder.Property(x => x.Description);
        builder.Property(x => x.Title);
        builder.Property(x => x.StartWorkTimeLocal);
        builder.Property(x => x.EndWorkTimeLocal);
        builder.Property(x => x.KitchenType);

        builder.OwnsOne(x => x.Contact,
            b => b.ToJson());
        builder.OwnsMany(x => x.MenuItems,
            b => b.ToJson());
        builder.OwnsMany(x => x.Pictures,
            b => b.ToJson());
    }
}