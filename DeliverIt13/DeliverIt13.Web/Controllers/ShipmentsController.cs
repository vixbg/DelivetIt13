using DeliverIt13.Data.Enums;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models.ShipmentDTOs;
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
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService shipmentService;
        private readonly IAuthHelper authHelper;

        public ShipmentsController(IShipmentService shipmentService, IAuthHelper authHelper)
        {
            this.shipmentService = shipmentService;
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
                var shipment = this.shipmentService.Get(id);
                return Ok(shipment);
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

                return Ok(this.shipmentService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromHeader] string credentials, [FromBody] ShipmentCreateDTO shipment)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }

                this.shipmentService.Create(shipment);
                return Created("Shipment created", shipment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }

                this.shipmentService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("")]
        public IActionResult Put([FromHeader] string credentials, [FromBody] ShipmentUpdateDTO shipment)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }

                var updatedShipment = this.shipmentService.Update(shipment);
                return Ok(updatedShipment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("status/{id}")]
        public IActionResult GetStatus([FromHeader] string credentials, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                var shipment = this.shipmentService.GetStatus(id);
                return Ok(shipment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);                
            }
        }

        [HttpGet("next/{id}")]
        public IActionResult GetNextToArrive([FromHeader] string credentials, int warehouseId)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                var shipment = this.shipmentService.GetNextToArrive(warehouseId);
                return Ok(shipment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);                
            }
        }
        
     
        // api/shipments/byWarehouse/London
        [HttpGet("byWarehouse/{name}")]
        public IActionResult GetAllByWarehouse([FromHeader] string credentials, [FromRoute] string name)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }

                return Ok(this.shipmentService.GetAllFiltered(s => s.DepartureWarehouse.City.Name == name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //TODO--
        [HttpGet("byCustomer/{name}")]
        public IActionResult GetAllByCustomer([FromHeader] string credentials, [FromRoute] string name)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }

                return Ok(this.shipmentService.GetAllFiltered(s => s.DepartureWarehouse.City.Name == name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // api/shipmentsOnTheWay/Sofia
        [HttpGet("shipmentsOnTheWay/{cityName}")]
        public IActionResult GetOnTheWay([FromHeader] string credentials, string cityName)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                var shipment = this.shipmentService.GetOnTheWay(cityName);
                return Ok(shipment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);                
            }
        }

        

        
    }
}
