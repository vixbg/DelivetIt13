using System;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using DeliverIt13.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService cityService;
        private readonly IAuthHelper authHelper;

        public CitiesController(ICityService cityService, IAuthHelper authHelper)
        {
            this.cityService = cityService;
            this.authHelper = authHelper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id,[FromHeader] string authorization)
        {
            try
            {
                this.authHelper.TryGetUser(authorization);
                CityDTO city = this.cityService.Get(id);
                return Ok(city);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
