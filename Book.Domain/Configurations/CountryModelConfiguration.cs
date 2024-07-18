using Book.Domain.Configurations.Abstract;
using Book.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.Domain.Configurations;

public class CountryModelConfiguration : BaseModelConfiguration<CountryModel>
{
    public override void Configure(EntityTypeBuilder<CountryModel> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(c => c.Authors)
            .WithOne(a => a.Country)
            .HasForeignKey(a => a.CountryId);
    }
}