using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService cityService;

        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                CityDTO city = this.cityService.Get(id);
                return Ok(city);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }


        [HttpGet("")]
        public IActionResult GetAll()
        {
            var cities = this.cityService.GetAll();
            return Ok(cities);
        }
    }
}
