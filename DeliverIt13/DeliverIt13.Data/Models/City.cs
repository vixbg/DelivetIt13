using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverIt13.Data.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required, StringLength(85, MinimumLength = 3, ErrorMessage = "City name must be between 3 and 85 characters.")]
        public string Name { get; set; }
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

    }
}
