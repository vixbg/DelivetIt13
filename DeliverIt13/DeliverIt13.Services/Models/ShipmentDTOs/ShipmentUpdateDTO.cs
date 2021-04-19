using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Models.ShipmentDTOs
{
    public class ShipmentUpdateDTO
    {
        //status
        //parcels removal

        public ShipmentUpdateDTO(Shipment shipment)
        {
            this.Status = shipment.Status;
            this.Parcels.AddRange(shipment.Parcels);
        }
        public int ShipmentId { get; set; }
        
        public ShipmentStatus Status { get; set; }
        public List<Parcel> Parcels { get; set; } = new List<Parcel>();
        
    }
}
