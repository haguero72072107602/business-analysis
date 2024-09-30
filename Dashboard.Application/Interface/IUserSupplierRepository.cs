using Dashboard.Domain.Entities;

namespace Dashboard.Application.Interface;

public interface IUserSupplierRepository : IRepository<UserSupplier>
{
    Task<int> InsertUserSupplierAsync(int idUser, int idSupplier);
    Task<int> DeleteSupplierUserAsync(int idSupplier, int idUser);
}