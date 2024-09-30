using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Repository;

public class ProductOfferingRepository : Repository<ProductOffering>, IProductOfferingRepository
{
    private readonly AppDbContext _context;
    private readonly ICategoriesRepository _categoriesRepository;


    public ProductOfferingRepository(AppDbContext context, ICategoriesRepository categoriesRepository) : base(context)
    {
        _context = context;
        _categoriesRepository = categoriesRepository;
    }

    public async Task LoadDataCsvAsync(int idSupplier, IList<ProductOffering> productOfferings)
    {
        try
        {
            foreach (var productOffering in productOfferings)
            {
                if (productOffering.Cat!.Trim() != "")
                {
                    var category = await _categoriesRepository.FindCategoryAsync(productOffering.Cat!);

                    if (category == null)
                    {
                        await _categoriesRepository.AddAsync(new Categories()
                        {
                            Description = productOffering.Cat!
                        });
                    }
                }

                var item =  await _context.TbProductOffering
                    .Where(p => p.Supc!.ToUpper().Trim() == productOffering.Supc!.ToUpper().Trim())
                    .FirstOrDefaultAsync();

                if (item == null)
                {
                    await _context.AddAsync(productOffering);    
                }
                else
                {
                    item.Price = productOffering.Price;
                    item.Stock = productOffering.Stock;
                    _context.Update(item);
                }
            }
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw ;
        }
    }

    public async Task<IList<ProductOffering>> ReportPriceComparisonAsync(int idMup)
    {
        return await _context.TbProductOffering
            .Where(p => p.IdMup == idMup)
            .OrderBy( p=> p.Price)
            .ToListAsync();
    }
}