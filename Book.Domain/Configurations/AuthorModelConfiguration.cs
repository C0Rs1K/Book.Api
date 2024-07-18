using Book.Domain.Configurations.Abstract;
using Book.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.Domain.Configurations;

public class AuthorModelConfiguration : BaseModelConfiguration<AuthorModel>
{
    public override void Configure(EntityTypeBuilder<AuthorModel> builder)
    {
        base.Configure(builder);

        builder.ToTable("Authors");

        builder.Property(a => a.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.BirthDate)
            .IsRequired();

        builder.HasOne(a => a.Country)
            .WithMany()
            .HasForeignKey(a => a.CountryId)
            .IsRequired();

        builder.HasMany(a => a.Books)
            .WithOne(a => a.Author)
            .HasForeignKey(a => a.AuthorId)
            .IsRequired();
    }
}