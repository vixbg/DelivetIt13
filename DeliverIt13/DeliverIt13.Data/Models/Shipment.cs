using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeliverIt13.Data.Enums;

namespace DeliverIt13.Data.Models
{
    public class Shipment
    {
        [Key]
        public int ShipmentId { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public DateTime ArrivalDate { get; set; }
        [Required]
        public ShipmentStatus Status { get; set; }

        public List<Parcel> Parcels { get; set; } = new List<Parcel>();
        public int DepartureWarehouseId { get; set; }
        [ForeignKey(nameof(DepartureWarehouseId))]
        public Warehouse DepartureWarehouse { get; set; }
        public int ArrivalWarehouseId { get; set; }
        [ForeignKey(nameof(ArrivalWarehouseId))]
        public Warehouse ArrivalWarehouse { get; set; }

    }
}
