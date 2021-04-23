using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models.ShipmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DeliverIt13.Services
{
    public class ShipmentService : IShipmentService
    {
        
        private readonly DeliverItContext dbContext;
        
        public ShipmentService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
            
        }
        public ShipmentGetDTO Get(int id)
        {
            var shipment = this.dbContext.Shipments
                .Include(s => s.DepartureWarehouse)
                .ThenInclude(w=>w.City)
                .ThenInclude(c=>c.Country)
                .Include(s => s.ArrivalWarehouse)
                .ThenInclude(w=>w.City)
                .ThenInclude(c=>c.Country)
                .FirstOrDefault(s => s.ShipmentId == id);

            if (shipment == null)
            {
                throw new Exception("No shipments found with this ID.");
            }

            var newDTO = new ShipmentGetDTO(shipment);

            return newDTO;
        }
        public List<ShipmentGetDTO> GetAll()
        {
            var shipments = this.dbContext.Shipments
                .Include(s => s.DepartureWarehouse)
                .ThenInclude(w=>w.City)
                .ThenInclude(c=>c.Country)
                .Include(s => s.ArrivalWarehouse)
                .ThenInclude(w=>w.City)
                .ThenInclude(c=>c.Country)
                .ToList();

            if (shipments.Count == 0)
            {
                throw new Exception("No shipments found.");
            }

            var shipmentsDTOs = new List<ShipmentGetDTO>();            

            foreach (var shipment in shipments)
            {
                shipmentsDTOs.Add(new ShipmentGetDTO(shipment));
            }

            return shipmentsDTOs;
        }

        public ShipmentCreateDTO Create(ShipmentCreateDTO shipment)
        {
            if (shipment == null)
            {
                throw new Exception("Input Shipment is Empty or Null.");
            }

            var newShipment = new Shipment
            {
                DepartureDate = shipment.DepartureDate,
                ArrivalDate = shipment.ArrivalDate,
                Status = shipment.Status,
                DepartureWarehouseId = shipment.DepartureWarehouseId,
                ArrivalWarehouseId = shipment.ArrivalWarehouseId
            };

            this.dbContext.Shipments.Add(newShipment);
            this.dbContext.SaveChanges();

            return shipment;
        }

        public bool Delete(int id)
        {
            var shipment = this.dbContext.Shipments.FirstOrDefault(sh => sh.ShipmentId == id);

            if (shipment == null)
            {
                throw new Exception("No shipment found with this ID.");
            }

            this.dbContext.Shipments.Remove(shipment);
            this.dbContext.SaveChanges();

            return true;
        }

        public ShipmentUpdateDTO Update(ShipmentUpdateDTO shipmentDTO)
        {
            if (shipmentDTO == null)
            {
                throw new Exception("Input Shipment is Empty or Null.");
            }

            var shipment = this.dbContext.Shipments.FirstOrDefault(sh => sh.ShipmentId == shipmentDTO.ShipmentId);

            if (shipment == null)
            {
                throw new Exception("No shipment found with this ID.");
            }

            shipment.Status = shipmentDTO.Status;
            shipment.DepartureDate = shipmentDTO.DepartureDate;
            shipment.ArrivalDate = shipmentDTO.ArrivalDate;
            shipment.DepartureWarehouseId = shipmentDTO.DepartureWarehouseId;
            shipment.ArrivalWarehouseId = shipmentDTO.ArrivalWarehouseId;

            this.dbContext.SaveChanges();

            return shipmentDTO;
        }

        public ShipmentPublicDTO GetNextToArrive(int warehouseId)
        {
            DateTime todaysDate = DateTime.Today;

            var nextShipment = this.dbContext.Shipments
                .Where(sh => sh.ArrivalDate >= todaysDate && sh.ArrivalWarehouseId == warehouseId)
                .OrderBy(sh => sh.ArrivalDate)
                .Take(1)
                .FirstOrDefault();

            if (nextShipment == null)
            {
                throw new Exception("There are no next shipments.");
            }

            return new ShipmentPublicDTO(nextShipment);
        }

        public ShipmentPublicDTO GetStatus(int id)
        {
            var shipment = this.dbContext.Shipments.FirstOrDefault(sh => sh.ShipmentId == id);

            if (shipment == null)
            {
                throw new Exception("No shipment found with this ID.");
            }

            var shipmentDTO = new ShipmentPublicDTO(shipment);

            return shipmentDTO;
        }
        
        public List<ShipmentPublicDTO> GetAllFiltered(Expression<Func<Shipment, bool>> predicate)
        {
            var shipments = this.dbContext.Shipments.Where(predicate).ToList();

            var shipmentsDTOs = new List<ShipmentPublicDTO>();

            foreach (var shipment in shipments)
            {
                shipmentsDTOs.Add(new ShipmentPublicDTO(shipment));
            }

            return shipmentsDTOs;
        }

        public int GetOnTheWay(string cityName)
        {
            var shipments = this.dbContext.Shipments.Where(s=> s.ArrivalWarehouse.City.Name == cityName).ToList();

            return shipments.Count;
        }       
        
    }
    
}
