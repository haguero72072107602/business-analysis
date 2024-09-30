using Dashboard.Domain.Entities;

namespace Dashboard.Application.Interface;

public interface ICategoriesRepository : IRepository<Categories>
{
    Task<Categories?> FindCategoryAsync(string category);
}