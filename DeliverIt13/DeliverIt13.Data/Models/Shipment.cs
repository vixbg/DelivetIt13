using DeliverIt13.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT_DemoProject.Models
{
    public class Shipment
    {
        public int ShipmentId { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public int StatusId { get; set; }

        public Status Status { get; set; }

        public List<Parcel> Parcels { get; set; }

    }
}
