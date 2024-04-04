using BookService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookService.Infrastructure.Storage.TypeConfigurations;

public class AuthorizationTokenTypeConfiguration : IEntityTypeConfiguration<AuthorizationToken>
{
    public void Configure(EntityTypeBuilder<AuthorizationToken> builder)
    {
        builder.Property(x => x.Value).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.ExpiresAtUtc).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);
    }
}