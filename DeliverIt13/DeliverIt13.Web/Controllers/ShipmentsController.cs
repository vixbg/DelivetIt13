using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models.ShipmentDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentsController : Controller
    {
        private readonly IShipmentService shipmentService;

        public ShipmentsController(IShipmentService shipmentService)
        {
            this.shipmentService = shipmentService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var shipment = this.shipmentService.GetStatus(id);
                return Ok(shipment);
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
                var count = this.shipmentService.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] ShipmentDTO shipment)
        {
            try
            {
                this.shipmentService.Create(shipment);
                return Created("Shipment created", shipment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("")]
        public IActionResult Put([FromBody] ShipmentUpdateDTO shipment, int id)
        {
            try
            {
                var updatedShipment = this.shipmentService.Update(id, shipment);
                return Ok(updatedShipment);
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
                this.shipmentService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
