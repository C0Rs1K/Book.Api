using Book.Domain;
using Book.Domain.Models.Abstract;
using Book.Infrastructure.Database;
using Book.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Book.Infrastructure.Repositories;

public abstract class BaseRepository<T>(BookDbContext context) : IBaseRepository<T> where T : BaseModel
{
    public virtual void Create(T entity)
    {
        context.Set<T>().Add(entity.AsCreated());
    }

    public virtual void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public virtual IQueryable<T> GetRange()
    {
        return context.Set<T>().AsNoTracking();
    }

    public virtual async Task<T?> GetFirstByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Set<T>().Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public virtual void Update(T entity)
    {
        context.Set<T>().Update(entity.AsModified());
    }
}