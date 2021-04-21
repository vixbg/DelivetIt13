using System;
using System.Collections.Generic;
using System.Text;
using DeliverIt13.Data.Enums;

namespace DeliverIt13.Services.Models.ParcelDTOs
{
    public class ParcelFilterDTO
    {
        
        public double? Weight { get; set; }
        public string Customer { get; set; }
        public string Warehouse { get; set; }
        public Categories? Category { get; set; }
    }
}
