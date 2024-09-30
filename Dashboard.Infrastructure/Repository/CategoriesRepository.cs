using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Repository;

public class CategoriesRepository : Repository<Categories>, ICategoriesRepository
{
    private readonly AppDbContext _context;

    public CategoriesRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Categories?> FindCategoryAsync(string category)
    {
        return await _context.TbCategories
            .Where(p => p.Description.ToUpper().Trim().Contains(category.ToUpper().Trim()))
            .FirstOrDefaultAsync();
    }
}