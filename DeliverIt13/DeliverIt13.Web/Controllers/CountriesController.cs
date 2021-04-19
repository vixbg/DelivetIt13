using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeliverIt13.Data;
using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CountryDTO country = this.countryService.Get(id);
            return Ok(country);
        }
        
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var countries = this.countryService.GetAll();
            return Ok(countries);
        }




        //[HttpPost()]
        //public IActionResult Post([FromBody] CustomerCreateDto dto)
        //{
        //    var customer = new Customer()
        //    {
        //        CityId = 1,
        //        FirstName =  dto.FirstName, //"Ivan",
        //        LastName = dto.LastName, // "Ivanov",
        //        Street = dto.Stret, // "Ivanova str 1",
        //        User = new User()
        //        {
        //            Type = UserType.Customer,
        //            Email = dto.Email, // "ivan@yahoo.com",
        //            Password = dto.Password // "asdf"
        //        }
        //    };

        //    var db = new DeliverItContext();
        //    db.Customers.Add(customer);
        //    db.SaveChanges();
        //}

        
    }
}
