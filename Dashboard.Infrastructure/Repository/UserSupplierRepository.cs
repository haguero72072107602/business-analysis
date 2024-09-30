using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Repository;

public class UserSupplierRepository : Repository<UserSupplier>, IUserSupplierRepository 
{
    private readonly AppDbContext _context;

    public UserSupplierRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<int> InsertUserSupplierAsync(int idUser, int idSupplier)
    {
        await AddAsync(new UserSupplier()
        {
            idSupplier = idSupplier,
            idUser = idUser
        });

        return await SaveAsync();
    }

    public async Task<int> DeleteSupplierUserAsync(int idSupplier, int idUser)
    {
        return await _context.Database
            .ExecuteSqlAsync($"delete from UserSupplier where idSupplier = {idSupplier} and idUser = {idUser}");
    }
}