using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeliverIt13.Data.Enums;

namespace DeliverIt13.Data.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }
        [Required]
        public WarehouseType Type { get; set; }
        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Street name must be between 3 and 100 characters long!")]
        public string Street { get; set; }

    }
}
