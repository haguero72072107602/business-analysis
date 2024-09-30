using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Repository;

public class MUPRepository : Repository<MUP>, IMUPRepository
{
    private readonly AppDbContext _context;

    public MUPRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}