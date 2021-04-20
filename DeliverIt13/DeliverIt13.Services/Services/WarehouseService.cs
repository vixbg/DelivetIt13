using System;
using System.Collections.Generic;
using System.Linq;
using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using Microsoft.EntityFrameworkCore;


namespace DeliverIt13.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly DeliverItContext dbContext;

        public WarehouseService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public WarehouseGetDTO Get(int id)
        {
            if (id == null)
            {
                throw new NullReferenceException("ID cannot be Null or Empty.");

            }
            var warehouse = this.dbContext.Warehouses
                .Include(w => w.City)
                .ThenInclude(c => c.Country)
                .FirstOrDefault(w => w.WarehouseId == id);
            if (warehouse == null)
            {
                throw new NullReferenceException("No warehouse found with this ID.");
            }
            var newDTO = new WarehouseGetDTO(warehouse);
            return newDTO;
        }

        public List<WarehousePublicDTO> GetAll()
        {
            var warehouses = this.dbContext
                .Warehouses
                .Include(w => w.City)
                .ThenInclude(c => c.Country)
                .ToList();

            if (warehouses == null)
            {
                throw new NullReferenceException("No warehouses found.");
            }

            var warehouseDTOs = new List<WarehousePublicDTO>();
            foreach (var warehouse in warehouses)
            {
                var newDTO = new WarehousePublicDTO(warehouse);
                warehouseDTOs.Add(newDTO);
            }

            return warehouseDTOs;
        }
        
        public WarehouseCreateDTO Create(WarehouseCreateDTO warehouse)
        {
            if (warehouse == null)
            {
                throw new NullReferenceException("Input Warehouse is Empty or Null.");
            }
            var newWarehouse = new Warehouse();
            newWarehouse.Type = warehouse.WarehouseType;
            newWarehouse.CityId = warehouse.CityId;
            newWarehouse.Street = warehouse.Street;
            
            this.dbContext.Warehouses.Add(newWarehouse);
            this.dbContext.SaveChanges();
            
            return warehouse;
        }

        public void Delete(int id)
        {
            if (id == null)
            {
                throw new NullReferenceException("ID cannot be Null or Empty.");

            }
            var warehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.WarehouseId == id);
            if (warehouse == null)
            {
                throw new NullReferenceException("No warehouse found with this ID.");
            }

            this.dbContext.Warehouses.Remove(warehouse);
            this.dbContext.SaveChanges();

            return;

        }

        public WarehouseUpdateDTO Update(WarehouseUpdateDTO warehouseDTO)
        {
            if (warehouseDTO == null)
            {
                throw new NullReferenceException("Input Warehouse is Empty or Null.");
            }
            var warehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.WarehouseId == warehouseDTO.WarehouseId);

            if (warehouse == null)
            {
                throw new NullReferenceException("No warehouse found with this ID.");
            }

            warehouse.CityId = warehouseDTO.CityId;
            warehouse.Street = warehouseDTO.Street;
            warehouse.Type = warehouseDTO.WarehouseType;
            this.dbContext.SaveChanges();

            return warehouseDTO;
        }
    }
}