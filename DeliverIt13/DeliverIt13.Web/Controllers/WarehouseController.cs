using System;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var warehouse = this.warehouseService.Get(id);
                return Ok(warehouse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            try
            {
                var warehouses = this.warehouseService.GetAll();
                return Ok(warehouses);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] WarehouseDTO warehouse)
        {

            try
            {
                this.warehouseService.Create(warehouse);
                return Created("Warehouse Created", warehouse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.warehouseService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("")]
        public IActionResult Put([FromBody] WarehouseUpdateDTO warehouse)
        {
            try
            {
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