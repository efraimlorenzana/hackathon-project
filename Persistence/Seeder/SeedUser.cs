using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Seeder
{
    public static class SeedUser
    {
        public static async Task Initialize(DataContext context, UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var People = new List<Person>
                {
                    new Person { 
                        Id=Guid.Parse("52e45def-3f01-4e6b-b67c-16265ff230ae"), 
                        Lastname="Lorenzana", 
                        Firstname="Efraim", 
                        Middlename="Apostol",
                        Gender=Gender.Male,
                        Address="Taytay Rizal"
                    },
                    new Person { 
                        Id=Guid.Parse("827b8171-d4e3-4044-aedc-280c84170071"), 
                        Lastname="Gara", 
                        Firstname="Christian", 
                        Middlename="B",
                        Gender=Gender.Male,
                        Address="Makati City"
                    }
                };

                var Users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = People[0].Id.ToString(),
                        Fullname = People[0].Firstname + " " + People[0].Middlename.Substring(0, 1).ToUpper() + ". " + People[0].Lastname,
                        Person = People[0],
                        Email = "em@test.com",
                        UserName = "em",
                        Role = context.Roles.Where(x => x.Name == "Retailer").FirstOrDefault()
                    },
                    new AppUser
                    {
                        Id = People[1].Id.ToString(),
                        Fullname = People[1].Firstname + " " + People[1].Middlename.Substring(0, 1).ToUpper() + ". " + People[1].Lastname,
                        Person = People[1],
                        Email = "chris@test.com",
                        UserName = "christian",
                        Role = context.Roles.Where(x => x.Name == "Customer").FirstOrDefault()
                    }
                };

                // var Roles = new [] {"Retailer", "Customer"};
                // int role = 0;

                foreach(var user in Users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }
        }
    }
}