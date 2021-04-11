using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
