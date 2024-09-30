using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.Domain.Models;
using Dashboard.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Repository;

public class UserRepository : Repository<Users>, IUserRepository 
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Users?> LoginAsync(DtoUser? userRequest)
    {
        var findUser = await _context.TbUsers
            .Where(p => userRequest != null && p.UserName.ToUpper().Trim() == userRequest.UserName!.ToUpper().Trim())
            .FirstOrDefaultAsync();
 
        if (findUser == null) return null;

        if (await Task.FromResult((findUser?.Password.ToUpper().Trim() == userRequest?.Password?.ToUpper().Trim())))
        {
            return await Task.FromResult(findUser);
        }
        
        return null;    
    }
}