using Blazor.SubtleCrypto;
using Dashboard.Domain.Common;
using Dashboard.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Infrastructure.Context;

public class DbInitializer
{
    public static void Initialize(AppDbContext context, IServiceProvider servicesProvider)
        {
            Seed(context, servicesProvider).GetAwaiter().GetResult();
        }

        private static async Task Seed(AppDbContext context, IServiceProvider servicesProvider)
        {
            var encryptService = servicesProvider.GetRequiredService<ICryptoService>();

            if (!context.TbUsers.Any())
            {
                try
                {
                    //var password = await encryptService.EncryptAsync("Admin2024*");
                    await context.TbUsers.AddAsync(new ()
                    {
                        UserName = Roles.AdministratorRole,
                        Role = Roles.AdministratorRole,
                        Password =  "Admin2024*",
                    });

                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }

            /*
            var products = new Product[] {
                new Product { Group = "Consumer Drink", Sku = "Black Coffee", Cost = 3 },
                new Product { Group = "Consumer Drink", Sku = "Iced Coffee", Cost = 3.5 },
                new Product { Group = "Consumer Drink", Sku = "Hot Chocolate", Cost = 2 },
                new Product { Group = "Consumer Drink", Sku = "Cappuccino", Cost = 4 },
                new Product { Group = "Consumer Drink", Sku = "Hot Tea", Cost = 2 },
                new Product { Group = "Consumer Drink", Sku = "Espresso", Cost = 7 },
                new Product { Group = "Consumer Drink", Sku = "Iced Tea", Cost = 1.5 },
                new Product { Group = "Consumer Food ", Sku = "Donut", Cost = 1 },
                new Product { Group = "Consumer Food ", Sku = "Muffin", Cost = 2 },
                new Product { Group = "Consumer Food ", Sku = "Bagel", Cost = 0.99 },
                new Product { Group = "Consumer Food ", Sku = "Croissant", Cost = 2 },
                new Product { Group = "Consumer Food ", Sku = "Signature Sandwich" , Cost = 7 },
                new Product { Group = "Whole Product ", Sku = "Classic Roast Beans", Cost = 15 },
                new Product { Group = "Whole Product ", Sku = "French Vanilla K-Cups", Cost = 13 },
                new Product { Group = "Whole Product ", Sku = "Green Tea", Cost = 12 },
                new Product { Group = "Accessory", Sku = "Gift Card", Cost = 25 },
                new Product { Group = "Accessory", Sku = "Gift Card", Cost = 50 },
                new Product { Group = "Accessory", Sku = "Store T-Shirt" , Cost = 20 },
                new Product { Group = "Accessory", Sku = "Store Hat", Cost = 15 },
                new Product { Group = "Accessory", Sku = "Store Koozie (5 pack)" , Cost = 5 }
                };

            context.Products.AddRange(products);
            context.SaveChanges();
            */
        }
}