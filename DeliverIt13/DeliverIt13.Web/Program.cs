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
        /// <summary>
        /// Seeds JSON data from Seed folder.
        /// </summary>
        /// <param name="host">Current Build of the Solution</param>
        private static void MigrateDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<DeliverItContext>();
            dbContext.Database.Migrate();

            using var transaction = dbContext.Database.BeginTransaction();

            if (!dbContext.Countries.Any() && File.Exists(@"Seed\Countries.json"))
            {
                SetIdentityInsert<Country>(dbContext, true);
                dbContext.Countries.AddRange(JsonConvert.DeserializeObject<List<Country>>(File.ReadAllText(@"Seed\Countries.json")));
                dbContext.SaveChanges();
                SetIdentityInsert<Country>(dbContext, false);
            }

            if (!dbContext.Cities.Any() && File.Exists(@"Seed\Cities.json"))
            {
                SetIdentityInsert<City>(dbContext, true);
                dbContext.Cities.AddRange(JsonConvert.DeserializeObject<List<City>>(File.ReadAllText(@"Seed\Cities.json")));
                dbContext.SaveChanges();
                SetIdentityInsert<City>(dbContext, false);
            }

            if (!dbContext.Users.Any() && File.Exists(@"Seed\Users.json"))
            {
                SetIdentityInsert<User>(dbContext, true);
                dbContext.Users.AddRange(JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Seed\Users.json")));
                dbContext.SaveChanges();
                SetIdentityInsert<User>(dbContext, false);

            }

            if (!dbContext.Warehouses.Any() && File.Exists(@"Seed\Warehouses.json"))
            {
                SetIdentityInsert<Warehouse>(dbContext, true);
                dbContext.Warehouses.AddRange(JsonConvert.DeserializeObject<List<Warehouse>>(File.ReadAllText(@"Seed\Warehouses.json")));
                dbContext.SaveChanges();
                SetIdentityInsert<Warehouse>(dbContext, false);
            }

            if (!dbContext.Employees.Any() && File.Exists(@"Seed\Employees.json"))
            {
                SetIdentityInsert<Employee>(dbContext, true);
                dbContext.Employees.AddRange(JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText(@"Seed\Employees.json")));
                dbContext.SaveChanges();
                SetIdentityInsert<Employee>(dbContext, false);

            }

            if (!dbContext.Customers.Any() && File.Exists(@"Seed\Customers.json"))
            {
                SetIdentityInsert<Customer>(dbContext, true);
                dbContext.Customers.AddRange(JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(@"Seed\Customers.json")));
                dbContext.SaveChanges();
                SetIdentityInsert<Customer>(dbContext, false);
            }

            if (!dbContext.Shipments.Any() && File.Exists(@"Seed\Shipments.json"))
            {
                SetIdentityInsert<Shipment>(dbContext, true);
                dbContext.Shipments.AddRange(JsonConvert.DeserializeObject<List<Shipment>>(File.ReadAllText(@"Seed\Shipments.json")));
                dbContext.SaveChanges();
                SetIdentityInsert<Shipment>(dbContext, false);
            }

            if (!dbContext.Parcels.Any() && File.Exists(@"Seed\Parcels.json"))
            {
                SetIdentityInsert<Parcel>(dbContext, true);
                dbContext.Parcels.AddRange(JsonConvert.DeserializeObject<List<Parcel>>(File.ReadAllText(@"Seed\Parcels.json")));
                dbContext.SaveChanges();
                SetIdentityInsert<Parcel>(dbContext, false);
            }

            transaction.Commit();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        /// <summary>
        /// Turns ON and OFF IDENTITY INSERT for specific database table.
        /// </summary>
        /// <typeparam name="TEntity">Generic that takes the form of the Model that will show which table to be used.</typeparam>
        /// <param name="dbContext">Database Instance</param>
        /// <param name="on">Boolean to change ON or OFF of the parameter.</param>
        private static void SetIdentityInsert<TEntity>(DbContext dbContext, bool on)
        {
            var entityType = dbContext.Model.FindEntityType(typeof(TEntity));
            var query =
                $"SET IDENTITY_INSERT dbo.{entityType.GetTableName()} {(on ? "ON" : "OFF")};";
            dbContext.Database.ExecuteSqlRaw(query);
        }
    }
}
