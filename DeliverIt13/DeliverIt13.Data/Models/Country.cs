using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIt13.Data.Models
{
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }

        [Required, StringLength(56, MinimumLength = 4, ErrorMessage = "Country name must be between 4 and 56 characters.")]
        public string Name { get; set; }

    }
}
