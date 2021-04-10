using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT_DemoProject.Models
{
    public class Country
    {
        public int CountryId { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Country name is not valid!")]
        public string Name { get; set; }

    }
}
