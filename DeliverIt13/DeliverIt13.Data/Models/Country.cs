using System;
using System.ComponentModel.DataAnnotations;

namespace DeliverIt13.Data.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required, StringLength(56, MinimumLength = 4, ErrorMessage = "Country name must be between 4 and 56 characters.")]
        public string Name { get; set; }

    }
}
