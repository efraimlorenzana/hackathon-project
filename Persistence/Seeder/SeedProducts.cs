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
                        Name = "Vivo Y17",
                        Category = "Mobile Phones",
                        Price = 1950,
                        Points = 19500,
                        IsAvailable = true
                    },
                    new Product
                    {
                        Id = Guid.Parse("6fb6a685-6303-469f-ac95-1ce884e912c1"),
                        Name = "TCL 32 in. 32S6500",
                        Category = "Appliances",
                        Price = 7995,
                        Points = 79950,
                        IsAvailable = true
                    },
                    new Product
                    {
                        Id = Guid.Parse("be8e128a-6378-407d-950e-d9fc42cc8d59"),
                        Name = "Kyowa KW-2142 Rice Cooker",
                        Category = "Appliances",
                        Price = 460,
                        Points = 4600,
                        IsAvailable = true
                    },
                    new Product
                    {
                        Id = Guid.Parse("e8a2e777-8a51-4110-b89d-d2883bc0fd85"),
                        Name = "Lenovo Core i5",
                        Category = "Computer & Accessories",
                        Price = 7999,
                        Points = 79990,
                        IsAvailable = true
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}