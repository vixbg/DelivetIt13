using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverIt13.Data.Enums;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models.CustomerDTOs;
using DeliverIt13.Services.Models.UserDTOs;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ICustomerService customerService;
        private readonly IWarehouseService warehouseService;

        public PublicController(IUserService userService, ICustomerService customerService, IWarehouseService warehouseService)
        {
            this.userService = userService;
            this.customerService = customerService;
            this.warehouseService = warehouseService;
        }

        /// <summary>
        /// Gets the count of all customers of DeliverIt13.
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetCount()
        {
            var custCount = customerService.GetCount();

            return Ok(custCount);
        }

        /// <summary>
        /// Gets all warehouses of DeliverIt13.
        /// </summary>
        /// <returns></returns>
        [HttpGet("warehouses")]
        public IActionResult GetWarehouses()
        {
            var warehouses = warehouseService.GetAllPublic();

            return Ok(warehouses);
        }

        /// <summary>
        /// Creates a user. Returns new User ID.
        /// </summary>
        /// <param name="userDTO">The user.</param>
        /// <returns></returns>
        [HttpPost("user")]
        public IActionResult PostUser([FromBody] UserCreatePublicDTO userDTO)
        {
            try
            {
                var id = this.userService.CreatePublic(userDTO);
                return Created("User Created", id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates a customer. Must have a user created first.
        /// </summary>
        /// <param name="customerDTO">The customer.</param>
        /// <returns></returns>
        [HttpPost("customer")]
        public IActionResult PostCustomer([FromBody] CustomerCreateDTO customerDTO)
        {
            try
            {
                this.customerService.Create(customerDTO);
                return Created("Customer Created", customerDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
