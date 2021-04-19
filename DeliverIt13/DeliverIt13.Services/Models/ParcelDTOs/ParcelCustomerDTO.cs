using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models.ParcelDTOs
{
    public class ParcelCustomerDTO
    {
        public ParcelCustomerDTO(Parcel parcel)
        {
            this.ParcelId = parcel.ParcelId;
            this.CustomerId = parcel.CustomerId;
            this.WarehouseId = parcel.WarehouseId;
            this.Weight = parcel.Weight;
            this.Category = parcel.Category;
            this.ShipmentId = parcel.ShipmentId;
        }

        //Check Rumi for What is needed in Customer.
        public int ParcelId { get; set; }
        public int CustomerId { get; set; }
        public int WarehouseId { get; set; }
        public double Weight { get; set; }
        public Categories Category { get; set; }
        public int ShipmentId { get; set; }
    }
}