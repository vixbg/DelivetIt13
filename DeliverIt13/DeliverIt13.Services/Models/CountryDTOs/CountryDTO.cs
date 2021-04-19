using System;
using System.Collections.Generic;
using System.Text;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models
{
    public class CountryDTO
    {
        public CountryDTO(Country country)
        {
            this.Name = country.Name;
            this.CountryId = country.CountryId;
        }
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}
