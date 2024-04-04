using BookService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookService.Infrastructure.Storage.TypeConfigurations;

public class ClientTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasIndex(x => x.Email).IsUnique();
        
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();
    }
}