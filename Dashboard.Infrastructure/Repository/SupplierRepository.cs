using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Repository;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository 
{
    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<int> DeleteSupplierAsync(int idSupplier)
    {
        return await _context.Database
            .ExecuteSqlAsync($"delete from Supplier where idSupplier = {idSupplier}");
    }
}