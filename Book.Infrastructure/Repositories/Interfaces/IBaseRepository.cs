using System.Linq.Expressions;
using Book.Domain.Models.Abstract;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Book.Infrastructure.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseModel
{
    public Task<T?> GetFirstByIdAsync(int id, CancellationToken cancellationToken);
    public IQueryable<T> GetRange();
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}