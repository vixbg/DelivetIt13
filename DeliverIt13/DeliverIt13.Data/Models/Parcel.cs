using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeliverIt13.Data.Enums;

namespace DeliverIt13.Data.Models
{
    public class Parcel
    {
        [Key]
        public int ParcelId { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
        public int WarehouseId { get; set; }
        [ForeignKey(nameof(WarehouseId))]
        public Warehouse Warehouse { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public Categories Category { get; set; }
        public int ShipmentId { get; set; }
        [ForeignKey(nameof(ShipmentId))]
        public Shipment Shipment { get; set; }

    }
}
