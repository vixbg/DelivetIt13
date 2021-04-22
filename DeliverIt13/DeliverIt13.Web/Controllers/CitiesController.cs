using System;
using DeliverIt13.Data.Enums;
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

        /// <summary>
        /// Gets the specified city.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="id">ID of the City.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string credentials,int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                CityGetDTO city = this.cityService.Get(id);
                return Ok(city);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all cities.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult GetAll([FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                var cities = this.cityService.GetAll();
                return Ok(cities);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
