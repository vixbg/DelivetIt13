using System;
using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models
{
    public class WarehouseGetDTO
    {

        public WarehouseGetDTO(Warehouse warehouse)
        {
            this.Country = warehouse.City.Country.Name;
            this.City = warehouse.City.Name;
            this.Street = warehouse.Street;
            this.WarehouseType = warehouse.Type;
            this.WarehouseId = warehouse.WarehouseId;
            this.CityId = warehouse.CityId;
            this.WarehouseTypeString = Enum.GetName(typeof(WarehouseType), warehouse.Type);
        }
        public int WarehouseId { get; set; }
        public string Country { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public WarehouseType WarehouseType { get; set; }
        public string WarehouseTypeString { get; set; }

    }
}