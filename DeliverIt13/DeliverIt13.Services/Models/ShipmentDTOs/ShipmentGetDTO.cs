using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Models.ShipmentDTOs
{
    public class ShipmentGetDTO
    {
        public ShipmentGetDTO() { }
        public ShipmentGetDTO(Shipment shipment)
        {
            this.ShipmentId = shipment.ShipmentId;
            this.DepartureDate = shipment.DepartureDate;
            this.ArrivalDate = shipment.ArrivalDate;
            this.Status = shipment.Status;
            this.DepartureWarehouseId = shipment.DepartureWarehouseId;
            this.DepartureWarehouse = shipment.DepartureWarehouse;
            this.ArrivalWarehouseId = shipment.ArrivalWarehouseId;
            this.ArrivalWarehouse = shipment.ArrivalWarehouse;
            this.StatusString = Enum.GetName(typeof(ShipmentStatus), shipment.Status);
        }
        public int ShipmentId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public ShipmentStatus Status { get; set; }
        public string StatusString { get; set; }
        public List<Parcel> Parcels { get; set; } = new List<Parcel>();
        public int DepartureWarehouseId { get; set; }
        public Warehouse DepartureWarehouse { get; set; }
        public int ArrivalWarehouseId { get; set; }
        public Warehouse ArrivalWarehouse { get; set; }
    }
}
