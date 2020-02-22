using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Seeder;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;

                try 
                {
                    var context = sp.GetRequiredService<DataContext>();
                    var userManager = sp.GetRequiredService<UserManager<AppUser>>();

                    context.Database.Migrate();
                    SeedUser.Initialize(context, userManager).Wait();
                    SeedProducts.Initialize(context).Wait();
                }
                catch (Exception err)
                {
                    var logger = sp.GetRequiredService<ILogger<Program>>();
                    logger.LogError(err, "Error migrating database");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
                // .ConfigureWebHostDefaults(webBuilder =>
                // {
                //     webBuilder.UseStartup<Startup>();
                // });
    }
}
