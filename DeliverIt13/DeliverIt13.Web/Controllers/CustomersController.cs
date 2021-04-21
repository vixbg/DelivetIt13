using DeliverIt13.Data.Enums;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models.CustomerDTOs;
using DeliverIt13.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        //crud - done
        //public - how many customers and post 
        //search by email
        //search by first/last name
        //see incoming parcels
        //search by multiple criteria ??????????
        //search all fields - nema shans 

        private readonly ICustomerService customerService;
        private readonly IAuthHelper authHelper;

        public CustomersController(ICustomerService customerService, IAuthHelper authHelper)
        {
            this.customerService = customerService;
            this.authHelper = authHelper;
        }

        //[HttpGet("filter")]
        public IActionResult GetAllFiltered([FromHeader] string credentials, [FromQuery] CustomerFilterDTO filter)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);

                var customers = this.customerService.GetAllFiltered(filter);
                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string credentials, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }

                var customer = this.customerService.Get(id);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                var count = this.customerService.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] CustomerCreateDTO customer)
        {
            try
            {
                this.customerService.Create(customer);
                return Created("User created", customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("")]
        public IActionResult Put([FromHeader] string credentials, [FromBody] CustomerUpdateDTO customer, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                
                var updatedCustomer = this.customerService.Update(id, customer);
                return Ok(updatedCustomer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromHeader] string credentials, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }

                this.customerService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
