using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Models.ParcelDTOs
{
    public class ParcelFilterDto
    {
        public double? Weight { get; set; }
        public string Customer { get; set; }
    }
}
