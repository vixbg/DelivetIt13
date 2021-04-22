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

        public CityGetDTO Get(int id)
        {
            City city = this.dbContext.Cities.FirstOrDefault(c => c.CityId.Equals(id));

            if (city == null)
            {
                throw new Exception("City was not found!");
            }

            CityGetDTO cityDTO = new CityGetDTO(city);

            return cityDTO;
        }


        public List<CityGetDTO> GetAll()
        {
            var cities = this.dbContext.Cities.ToList();

            if (cities.Count == 0)
            {
                throw new Exception("No cities found!");
            }

            List<CityGetDTO> citiesDTO = cities.Select(c => new CityGetDTO(c)).ToList();                    

            return citiesDTO;
        }
    }
}
