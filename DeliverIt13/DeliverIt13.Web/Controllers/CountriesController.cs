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

        [HttpGet("{id}")]
        public IActionResult Get(int id,[FromHeader] string credentials)
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
