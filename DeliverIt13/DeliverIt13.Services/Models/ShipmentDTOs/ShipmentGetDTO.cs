using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Models.ShipmentDTOs
{
    public class ShipmentGetDTO
    {
        public ShipmentGetDTO(Shipment shipment)
        {
            this.Status = shipment.Status;
        }
        public ShipmentStatus Status { get; set; }
    }
}
