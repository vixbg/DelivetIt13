using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DeliverIt13.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            MigrateDatabase(host);
            host.Run();
        }

        private static void MigrateDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<DeliverItContext>();
            
            dbContext.Database.Migrate();

            if (!dbContext.Countries.Any() && File.Exists(@"Seed\Countries.json"))
            {
                dbContext.Countries.AddRange(JsonConvert.DeserializeObject<List<Country>>(File.ReadAllText(@"Seed\Countries.json")));
                dbContext.SaveChanges();
            }

            if (!dbContext.Cities.Any() && File.Exists(@"Seed\Cities.json"))
            {
                dbContext.Cities.AddRange(JsonConvert.DeserializeObject<List<City>>(File.ReadAllText(@"Seed\Cities.json")));
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any() && File.Exists(@"Seed\Users.json"))
            {
                dbContext.Users.AddRange(JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Seed\Users.json")));
                dbContext.SaveChanges();
            }

            //if (!dbContext.Employees.Any() && File.Exists(@"Seed\Employees.json"))
            //{
            //    dbContext.Employees.AddRange(JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText(@"Seed\Employees.json")));
            //    dbContext.SaveChanges();
            //}

            if (!dbContext.Customers.Any() && File.Exists(@"Seed\Customers.json"))
            {
                dbContext.Customers.AddRange(JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(@"Seed\Customers.json")));
                dbContext.SaveChanges();
            }

            //if (!dbContext.Warehouses.Any() && File.Exists(@"Seed\Warehouses.json"))
            //{
            //    dbContext.Warehouses.AddRange(JsonConvert.DeserializeObject<List<Warehouse>>(File.ReadAllText(@"Seed\Warehouses.json")));
            //    dbContext.SaveChanges();
            //}

            //if (!dbContext.Shipments.Any() && File.Exists(@"Seed\Shipments.json"))
            //{
            //    dbContext.Shipments.AddRange(JsonConvert.DeserializeObject<List<Shipment>>(File.ReadAllText(@"Seed\Shipments.json")));
            //    dbContext.SaveChanges();
            //}

            //if (!dbContext.Parcels.Any() && File.Exists(@"Seed\Parcels.json"))
            //{
            //    dbContext.Parcels.AddRange(JsonConvert.DeserializeObject<List<Parcel>>(File.ReadAllText(@"Seed\Parcels.json")));
            //    dbContext.SaveChanges();
            //}


            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
