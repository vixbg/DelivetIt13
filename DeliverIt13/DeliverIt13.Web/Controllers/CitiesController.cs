using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly ICityService cityService;

        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CityDTO city = this.cityService.Get(id);

            return Ok(city);
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var cities = this.cityService.GetAll();
            return Ok(cities);
        }
    }
}
