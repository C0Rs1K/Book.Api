using Book.Domain.Configurations.Abstract;
using Book.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.Domain.Configurations;

public class GenreModelConfiguration : BaseModelConfiguration<GenreModel>
{
    public override void Configure(EntityTypeBuilder<GenreModel> builder)
    {
        base.Configure(builder);

        builder.Property(g => g.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(g => g.Books)
            .WithOne(b => b.Genre)
            .HasForeignKey(b => b.GenreId)
            .IsRequired();
    }
}