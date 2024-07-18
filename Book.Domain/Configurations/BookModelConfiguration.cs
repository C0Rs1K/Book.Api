using Book.Domain.Configurations.Abstract;
using Book.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.Domain.Configurations;

public class BookModelConfiguration : BaseModelConfiguration<BookModel>
{
    public override void Configure(EntityTypeBuilder<BookModel> builder)
    {
        base.Configure(builder);

        builder.ToTable("Books");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.ISBN)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Description)
            .HasMaxLength(500);

        builder.Property(b => b.TakeTime)
            .IsRequired();

        builder.Property(b => b.ReturnTime)
            .IsRequired();

        builder.HasOne(b => b.Genre)
            .WithMany()
            .HasForeignKey(b => b.GenreId);

        builder.HasOne(b => b.Author)
            .WithMany()
            .HasForeignKey(b => b.AuthorId);
    }
}