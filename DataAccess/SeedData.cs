using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Check if any products exist.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                context.Products.AddRange(
                    new Product.Builder()
                        .WithName("Product1")
                        .WithDescription("Description1")
                        .WithImageUrl("ImageUrl1")
                        .WithPrice(10.0M)
                        .WithCategory("Category1")
                        .Build(),

                    new Product.Builder()
                        .WithName("Product2")
                        .WithDescription("Description2")
                        .WithImageUrl("ImageUrl2")
                        .WithPrice(20.0M)
                        .WithCategory("Category2")
                        .Build()
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
