using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIt13.Data.Models
{
    public class City
    {
        [Key]
        public Guid CityId { get; set; }

        [Required, StringLength(85, MinimumLength = 3, ErrorMessage = "City name must be between 3 and 85 characters.")]
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

    }
}
