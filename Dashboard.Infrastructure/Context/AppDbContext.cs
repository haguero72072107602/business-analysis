using Dashboard.Domain.Entities;
using Dashboard.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Context;

public class AppDbContext : DbContext /*IdentityDbContext<ApplicationUser>*/ 
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Users> TbUsers { get; set; }
    public DbSet<MUP> TbMup { get; set; }
    public DbSet<Entities> TbEntities { get; set; }
    public DbSet<ProductOffering> TbProductOffering { get; set; }
    public DbSet<Supplier> TbPSupplier { get; set; }
    public DbSet<ProdSupplierRelationship> TbProdSupplierRelationship { get; set; }
    public DbSet<Categories?> TbCategories { get; set; }
    public DbSet<UserSupplier> TbUserSupplier { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            entry.Entity.ModifiedDateTime = DateTime.Now;
            
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreateDateTime = DateTime.Now;
            }
            
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedDateTime = DateTime.Now;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}

public class ApplicationUser : IdentityUser
{
    /*
    public ApplicationUser() { }

    public static ApplicationUserBuilder Builder()
    {
        return new ApplicationUserBuilder(new ApplicationUser());
    }

    public int AccountId { get; set; }
    public Guid AccountGuid { get; set; }
    public string Salt { get; set; }
    public bool Verified { get; set; }
    public string Checksum { get; set; }
    public string Password { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    */
}