using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.Infrastructure.Context;

namespace Dashboard.Infrastructure.Repository;

public class ProdSupplierRelationshipRepository : Repository<ProdSupplierRelationship>, 
    IProdSupplierRelationshipRepository
{
    public ProdSupplierRelationshipRepository(AppDbContext context) : base(context)
    {
    }
}