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
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService warehouseService;
        private readonly IAuthHelper authHelper;

        public WarehouseController(IWarehouseService warehouseService,IAuthHelper authHelper)
        {
            this.warehouseService = warehouseService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Gets the specified warehouse.
        /// </summary>
        /// <param name="id">Id of the warehouse.</param>
        /// <param name="credentials">User authorization - employee.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id,[FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized($"Unauthorized Username/Email: {credentials}");
                }
                var warehouse = this.warehouseService.Get(id);
                return Ok(warehouse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all warehouses - internal.
        /// </summary>
        /// <param name="credentials">User authorization - employee.</param>
        /// <returns></returns>
        [HttpGet("internal")]
        public IActionResult GetAll([FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized($"Unauthorized Username/Email: {credentials}");
                }
                var warehouses = this.warehouseService.GetAll();
                return Ok(warehouses);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all warehouses - public.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult GetAllPublic()
        {
            try
            {
                var warehouses = this.warehouseService.GetAllPublic();
                return Ok(warehouses);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates the specified warehouse.
        /// </summary>
        /// <param name="warehouse">The warehouse.</param>
        /// <param name="credentials">User authorization - employee.</param>
        /// <returns></returns>
        [HttpPost("")]
        public IActionResult Post([FromBody] WarehouseCreateDTO warehouse, [FromHeader] string credentials)
        {

            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized($"Unauthorized Username/Email: {credentials}");
                }
                this.warehouseService.Create(warehouse);
                return Created("Warehouse Created", warehouse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes the specified warehouse.
        /// </summary>
        /// <param name="id">Id of the warehouse.</param>
        /// <param name="credentials">User authorization - employee.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized($"Unauthorized Username/Email: {credentials}");
                }
                this.warehouseService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates the specified warehouse.
        /// </summary>
        /// <param name="warehouse">The warehouse.</param>
        /// <param name="credentials">User authorization - employee.</param>
        /// <returns></returns>
        [HttpPut("")]
        public IActionResult Put([FromBody] WarehouseUpdateDTO warehouse,[FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized($"Unauthorized Username/Email: {credentials}");
                }
                var updatedWarehouse = this.warehouseService.Update(warehouse);
                return Ok(updatedWarehouse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}