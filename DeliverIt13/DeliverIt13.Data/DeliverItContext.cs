using DeliverIt13.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliverIt13.Data
{
    public class DeliverItContext : DbContext
    {
        public DeliverItContext(DbContextOptions<DeliverItContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //City Cascade Fix
            modelBuilder.Entity<City>()
                .HasOne<Country>(s => s.Country)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            
            //Customer Cascade Fix
            modelBuilder.Entity<Customer>()
                .HasOne<City>(s => s.City)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            //Warehouse Cascade Fix
            modelBuilder.Entity<Warehouse>()
                .HasOne(s => s.City)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            //Employee Cascade Fix
            modelBuilder.Entity<Employee>()
                .HasOne(s => s.Warehouse)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            //Shipment Cascade Fix
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.ArrivalWarehouse)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.DepartureWarehouse)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            modelBuilder.Entity<Shipment>()
                .HasMany(s => s.Parcels)
                .WithOne(p => p.Shipment)
                .Metadata.DeleteBehavior = DeleteBehavior.Cascade;

            //Parcel Cascade Fix
            modelBuilder.Entity<Parcel>()
                .HasOne(s => s.Warehouse)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            modelBuilder.Entity<Parcel>()
                .HasOne(s => s.Shipment)
                .WithMany(p => p.Parcels)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            modelBuilder.Entity<Parcel>()
                .HasOne(s => s.Customer)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
    }
}
