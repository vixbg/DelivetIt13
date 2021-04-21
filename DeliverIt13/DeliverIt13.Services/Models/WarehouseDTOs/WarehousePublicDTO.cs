using System;
using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models
{
    public class WarehousePublicDTO
    {

        public WarehousePublicDTO(Warehouse warehouse)
        {
            this.Country = warehouse.City.Country.Name;
            this.City = warehouse.City.Name;
            this.Street = warehouse.Street;
            //this.WarehouseType = warehouse.Type;
            //this.WarehouseId = warehouse.WarehouseId;
        }
        //public int WarehouseId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        //public int CityId { get; set; }
        public string Street { get; set; }
        //public WarehouseType WarehouseType { get; set; }
        
    }
}