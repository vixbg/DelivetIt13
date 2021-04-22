using System;
using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models
{
    public class WarehouseUpdateDTO
    {

        public WarehouseUpdateDTO()
        {
            
        }
        public int WarehouseId { get; set; }
        public int CityId { get; set; }
        public string Street { get; set; }
        public WarehouseType WarehouseType { get; set; }
        
    }
}