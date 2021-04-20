using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models.ShipmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt13.Services
{
    public class ShipmentService : IShipmentService
    {
        /*
         CRUD operations (must) - done

        • Filter by warehouse (must)

        • Filter by customer (should)
         */
        private readonly DeliverItContext dbContext;

        public ShipmentService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ShipmentDTO Create(ShipmentDTO shipment)
        {
            if (shipment == null)
            {
                throw new ArgumentException("Cannot create an instance of an empty object.");
            }

            var newShipment = new Shipment();

            newShipment.ShipmentId = shipment.ShipmentId;
            newShipment.DepartureDate = shipment.DepartureDate;
            newShipment.ArrivalDate = shipment.ArrivalDate;
            newShipment.Status = shipment.Status;
            newShipment.Parcels = shipment.Parcels;
            newShipment.DepartureWarehouseId = shipment.DepartureWarehouseId;
            newShipment.ArrivalWarehouseId = shipment.ArrivalWarehouseId;

            this.dbContext.Shipments.Add(newShipment);
            this.dbContext.SaveChanges();

            return shipment;
        }

        public bool Delete(int id)
        {
            var shipment = this.dbContext.Shipments.FirstOrDefault(sh => sh.ShipmentId == id);

            if (shipment == null)
            {
                throw new NullReferenceException("No shipment found with this ID.");
            }

            this.dbContext.Shipments.Remove(shipment);
            this.dbContext.SaveChanges();

            return true;
        }

        public ShipmentDTO GetNextToArrive()
        {
            DateTime todaysDate = DateTime.Today;

            var shipments = this.GetAll().OrderBy(sh => sh.ArrivalDate);

            var nextShipment = shipments.FirstOrDefault(sh => sh.ArrivalDate.CompareTo(todaysDate) >= 0);

            if (nextShipment == null)
            {
                throw new ArgumentException("There are no next shipments.");
            }

            return nextShipment;
        }

        public List<ShipmentDTO> GetAll()
        {
            var shipments = this.dbContext.Shipments.ToList();

            var shipmentsDTOs = new List<ShipmentDTO>();

            foreach (var shipment in shipments)
            {
                shipmentsDTOs.Add(new ShipmentDTO(shipment));
            }

            return shipmentsDTOs;
        }

        public int GetCount()
        {
            var shipments = this.dbContext.Shipments.ToList();

            return shipments.Count;
        }

        public ShipmentPublicDTO GetStatus(int id)
        {
            var shipment = this.dbContext.Shipments.FirstOrDefault(sh => sh.ShipmentId == id);

            if (shipment == null)
            {
                throw new ArgumentException("No shipment found with this ID.");
            }

            var shipmentDTO = new ShipmentPublicDTO(shipment);

            return shipmentDTO;
        }

        public ShipmentUpdateDTO Update(int id, ShipmentUpdateDTO shipmentDTO)
        {
            if (shipmentDTO == null)
            {
                throw new ArgumentException("Input shipment cannot be null or empty!");
            }

            var shipment = this.dbContext.Shipments.FirstOrDefault(sh => sh.ShipmentId == id);

            if (shipment == null)
            {
                throw new ArgumentException("No shipment found with this ID.");
            }

            shipment.Status = shipmentDTO.Status;
            shipment.Parcels = shipmentDTO.Parcels;

            this.dbContext.SaveChanges();

            return shipmentDTO;
        }
    }
    
}
