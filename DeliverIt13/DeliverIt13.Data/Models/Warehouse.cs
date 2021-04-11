using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DeliverIt13.Data.Enums;

namespace DeliverIt13.Data.Models
{
    public class Warehouse
    {
        [Key]
        public Guid WarehouseId { get; set; }
        [Required]
        public WarehouseType Type { get; set; }
        public Guid CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        public Guid CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Street name must be between 3 and 100 characters long!")]
        public string Street { get; set; }

    }
}
