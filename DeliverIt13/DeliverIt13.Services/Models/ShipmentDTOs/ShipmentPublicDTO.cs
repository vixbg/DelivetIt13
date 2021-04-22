using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Models.ShipmentDTOs
{
    public class ShipmentPublicDTO
    {
        public ShipmentPublicDTO() {}
        public ShipmentPublicDTO(Shipment shipment)
        {
            this.ShipmentId = ShipmentId;
            this.Status = shipment.Status;
            this.StatusString = Enum.GetName(typeof(ShipmentStatus), shipment.Status);
        }

        public int ShipmentId { get; set; }
        public ShipmentStatus Status { get; set; }
        public string StatusString { get; set; }
    }
}
