using Book.Domain.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.Domain.Configurations.Abstract;

public class BaseModelConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseModel
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.CreatedDate).IsRequired();
        builder.Property(p => p.ModifiedDate);
        builder.Property(p => p.IsDeleted).IsRequired();
    }
}