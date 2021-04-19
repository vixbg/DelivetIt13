using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models.ParcelDTOs
{
    public class ParcelCreateDTO
    {
        public ParcelCreateDTO(Parcel parcel)
        {
            
            this.CustomerId = parcel.CustomerId;
            this.WarehouseId = parcel.WarehouseId;
            this.Weight = parcel.Weight;
            this.Category = parcel.Category;
            
        }

        public int ParcelId { get; set; }
        public int CustomerId { get; set; }
        public int WarehouseId { get; set; }
        public double Weight { get; set; }
        public Categories Category { get; set; }
        
    }
}