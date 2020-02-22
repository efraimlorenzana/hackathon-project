using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Seeder
{
    public static class SeedProducts
    {
        public static async Task Initialize(DataContext context)
        {
            if(!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.Parse("16ff64f1-c2c4-4c35-a5cc-be6c3e558297"),
                        Name = "Coffeelate",
                        Category = "Category 1",
                        Price = 100.00,
                        Points = 10
                    },
                    new Product
                    {
                        Id = Guid.Parse("6fb6a685-6303-469f-ac95-1ce884e912c1"),
                        Name = "Cappuccino",
                        Category = "Category 1",
                        Price = 120.00,
                        Points = 15
                    },
                    new Product
                    {
                        Id = Guid.Parse("be8e128a-6378-407d-950e-d9fc42cc8d59"),
                        Name = "Brewed Coffee",
                        Category = "Category 2",
                        Price = 100.00,
                        Points = 11
                    },
                    new Product
                    {
                        Id = Guid.Parse("e8a2e777-8a51-4110-b89d-d2883bc0fd85"),
                        Name = "Coffee Milk",
                        Category = "Category 2",
                        Price = 110.00,
                        Points = 13
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}