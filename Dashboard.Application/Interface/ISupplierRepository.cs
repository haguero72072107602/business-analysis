using Dashboard.Domain.Entities;

namespace Dashboard.Application.Interface;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<int> DeleteSupplierAsync(int idSupplier);
}