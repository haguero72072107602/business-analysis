using Blazor.SubtleCrypto;
using Dashboard.Application.Interface;
using Dashboard.Infrastructure.Context;
using Dashboard.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static System.Threading.Tasks.Task;


namespace Dashboard.Infrastructure.Extension;

public static class AddRegisterInfrastructure
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Dashboard"));
        });
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEntitiesRepository, EntitiesRepository>();
        services.AddScoped<IMUPRepository, MUPRepository>();
        services.AddScoped<IProductOfferingRepository, ProductOfferingRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IUserSupplierRepository, UserSupplierRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        
        services.AddSubtleCrypto(opt => 
                opt.Key = "ELE9xOyAyJHCsIPLMbbZHQ7pVy7WUlvZ60y5WkKDGMSw5xh5IM54kUPlycKmHF9VGtYUilglL8iePLwr" //Use another key
        );

        /*
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "cookie_auth";
                options.LoginPath = "/login";
                options.Cookie.MaxAge = TimeSpan.FromDays(1);
                options.AccessDeniedPath = "/access-denied";

            });
        
        services.AddIdentity<ApplicationUser, IdentityRole>(p =>
            {
                p.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.@ ";

                p.User.RequireUniqueEmail = true;
                p.Password.RequiredLength = 5;
                p.Password.RequireUppercase = false;
                p.Password.RequireLowercase = false;
                p.Password.RequireUppercase = false;
                p.Password.RequireNonAlphanumeric = false;
                p.Password.RequireDigit = false;

                p.SignIn.RequireConfirmedEmail = false;
            })
            //.AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        */    

        return services;
    }


    public static void RegisterAppInfrastructure(this WebApplication app )
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                var environment = services.GetRequiredService<IWebHostEnvironment>();
                context.Database.Migrate();
               DbInitializer.Initialize(context, services);
            }
            catch (Exception ex)
            {
                //var logger = services.GetRequiredService<ILogger<Program>>();
                //logger.LogError(ex, "An error occurred creating the DB.");
            }
        }

        /*
        using (var serviceScope = app.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
        }
        */
    }

}