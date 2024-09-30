using Dashboard.Domain.Entities;

namespace Dashboard.Application.Interface;

public interface IProductOfferingRepository : IRepository<ProductOffering>
{
    Task LoadDataCsvAsync(int idSupplier, IList<ProductOffering> productOfferings);
    Task<IList<ProductOffering>> ReportPriceComparisonAsync(int idMup);
}