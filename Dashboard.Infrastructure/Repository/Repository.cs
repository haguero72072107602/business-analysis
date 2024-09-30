using System.Linq.Expressions;
using Dashboard.Application.Interface;
using Dashboard.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dashboard.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public Repository( AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    
    public async Task<T?> GetAllAsync( Expression<Func<T, bool>>? filter )
    {
        return await _context.Set<T>().Where(filter!).FirstOrDefaultAsync();
    }

    public async Task<EntityEntry<T>> AddAsync(T data)
    {
        return await _context.Set<T>().AddAsync(data);
    }
    
    public EntityEntry<T> Update(T data)
    {
        return _context.Set<T>().Update(data);
    }
    
    public EntityEntry<T> Delete(T data)
    {
        return _context.Set<T>().Remove(data);
    }
    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}