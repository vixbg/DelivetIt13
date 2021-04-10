using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT_DemoProject.Models
{
    public class Parcel
    {
        public int ParcelId { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int Weight { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
