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

        private readonly ICustomerService customerService;
        private readonly IAuthHelper authHelper;
        private readonly IParcelService parcelService;

        public CustomersController(ICustomerService customerService, IAuthHelper authHelper, IParcelService parcelService)
        {
            this.customerService = customerService;
            this.authHelper = authHelper;
            this.parcelService = parcelService;
        }

        // api/customer?search=
        [HttpGet("")]
        public IActionResult GetAllSearch([FromHeader] string credentials,[FromQuery] string search, string searchBy)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                var customers = this.customerService.GetAllBySearch(search, searchBy);

                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("parcels")]
        public IActionResult GetAllParcels([FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Customer)
                {
                    return Unauthorized(credentials);
                }

                var customers = this.parcelService.GetAllCustomer(user);

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
        public IActionResult GetAll([FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                var customers = this.customerService.GetAll();
                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromHeader] string credentials,[FromBody] CustomerCreateDTO customer)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Customer)
                {
                    return Unauthorized(credentials);
                }
                this.customerService.Create(customer);
                return Created("User created", customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("")]
        public IActionResult Put([FromHeader] string credentials, [FromBody] CustomerUpdateDTO customer)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Customer)
                {
                    return Unauthorized(credentials);
                }
                
                var updatedCustomer = this.customerService.Update(customer);
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
                if (user.Type != UserType.Customer)
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
