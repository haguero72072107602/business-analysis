using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dashboard.Application.Interface;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAllAsync(Expression<Func<T, bool>>? filter);
    
    Task<EntityEntry<T>> AddAsync(T data);

    Task<int> SaveAsync();

    EntityEntry<T> Update(T data);
    EntityEntry<T> Delete(T data);
}