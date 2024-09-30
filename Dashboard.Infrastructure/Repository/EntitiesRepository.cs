using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.Infrastructure.Context;

namespace Dashboard.Infrastructure.Repository;

public class EntitiesRepository : Repository<Entities>, IEntitiesRepository
{
    public EntitiesRepository(AppDbContext context) : base(context)
    {
    }
}