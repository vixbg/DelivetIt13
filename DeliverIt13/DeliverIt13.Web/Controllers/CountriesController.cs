using System;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using Microsoft.AspNetCore.Mvc;
using DeliverIt13.Data.Enums;
using DeliverIt13.Web.Helpers;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;
        private readonly IAuthHelper authHelper;

        public CountriesController(ICountryService countryService,IAuthHelper authHelper)
        {
            this.countryService = countryService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Gets the specified Country.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="id">Id of the Country.</param>
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
                CountryDTO country = this.countryService.Get(id);
                return Ok(country);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Gets all countries.
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
                var countries = this.countryService.GetAll();
                return Ok(countries);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
