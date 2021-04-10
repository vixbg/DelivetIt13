using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT_DemoProject.Models
{
    public class City
    {
        public int CityId { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "City name is not valid!")]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

    }
}
