using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt13.Services
{
    public class CityService : ICityService
    {
        private readonly DeliverItContext dbContext;

        public CityService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CityDTO Get(int id)
        {
            City city = this.dbContext.Cities.FirstOrDefault(c => c.CityId.Equals(id));

            if (city == null)
            {
                throw new ArgumentException("City was not found!");
            }

            CityDTO cityDTO = new CityDTO(city);
            cityDTO.Name = city.Name;

            return cityDTO;
        }

        public List<CityDTO> GetAll()
        {
            var cities = this.dbContext.Cities.ToList();

            List<CityDTO> citiesDTO = new List<CityDTO>();

            foreach (var city in cities)
            {
                CityDTO cityDTO = new CityDTO(city);
                citiesDTO.Add(cityDTO);
            }

            return citiesDTO;
        }
    }
}
