using System;
using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models
{
    public class WarehouseCreateDTO
    {

        public WarehouseCreateDTO()
        {

        }

        public int CityId { get; set; }
        public string Street { get; set; }
        public WarehouseType WarehouseType { get; set; }
        
    }
}