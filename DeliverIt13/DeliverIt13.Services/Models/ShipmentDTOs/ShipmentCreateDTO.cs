using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Models.ShipmentDTOs
{
    public class ShipmentCreateDTO
    {
        public ShipmentCreateDTO() { }
        public ShipmentCreateDTO(Shipment shipment)
        {
            this.DepartureDate = shipment.DepartureDate;
            this.ArrivalDate = shipment.ArrivalDate;
            this.Status = shipment.Status;
            this.DepartureWarehouseId = shipment.DepartureWarehouseId;
            this.ArrivalWarehouseId = shipment.ArrivalWarehouseId;
        }
        
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public ShipmentStatus Status { get; set; }
        public int DepartureWarehouseId { get; set; }
        public int ArrivalWarehouseId { get; set; }
    }
}
