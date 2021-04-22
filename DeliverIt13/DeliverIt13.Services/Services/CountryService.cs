using System;
using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace DeliverIt13.Services
{
    public class CountryService : ICountryService
    {
        private readonly DeliverItContext dbContext;

        public CountryService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CountryDTO Get(int id)
        {

            Country country = this.dbContext.Countries.FirstOrDefault(c => c.CountryId.Equals(id));
            if (country == null)
            {
                throw new Exception("No country found with this ID.");
            }
            CountryDTO countryDTO = new CountryDTO(country);

            return countryDTO;
        }

        public List<CountryDTO> GetAll()
        {
            var countries = this.dbContext.Countries.ToList();

            if (countries.Count == 0)
            {
                throw new Exception("No countries found!");
            }

            List<CountryDTO> countryDTOs = new List<CountryDTO>();

            foreach (var country in countries)
            {
                CountryDTO countryDTO = new CountryDTO(country);
                countryDTOs.Add(countryDTO);
            }

            return countryDTOs;
        }
    }
}
