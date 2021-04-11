using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DeliverIt13.Data.Enums;

namespace DeliverIt13.Data.Models
{
    public class Parcel
    {
        [Key]
        public Guid ParcelId { get; set; }
        public Guid CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
        public Guid WarehouseId { get; set; }
        [ForeignKey(nameof(WarehouseId))]
        public Warehouse Warehouse { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public Categories Category { get; set; }
        public Guid ShipmentId { get; set; }
        [ForeignKey(nameof(ShipmentId))]
        public Shipment Shipment { get; set; }

    }
}
