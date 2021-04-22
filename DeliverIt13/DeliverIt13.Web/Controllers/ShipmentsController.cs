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

        /// <summary>
        /// Gets the specified shipment.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="id">Id of the Shipment.</param>
        /// <returns></returns>
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
                var shipment = this.shipmentService.Get(id);
                return Ok(shipment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all shipments.
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

                return Ok(this.shipmentService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates the specified shipment.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="shipment">The shipment that will be created.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes the specified shipment.- NOT WORKING
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="id">Id of the Shipment.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromHeader] string credentials,int id)
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

        /// <summary>
        /// Updates the specified shipment.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the status of a shipment.
        /// </summary>
        /// <param name="credentials">User authentication - employee or customer.</param>
        /// <param name="id">Id of the Shipment.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the next shipment to arrive at the warehouse.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="warehouseId">The warehouse ID.</param>
        /// <returns></returns>
        [HttpGet("next/{warehouseId}")]
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

        /// <summary>
        /// Gets all shipments by warehouse.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="name">The name of the warehouse.</param>
        /// <returns></returns>
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

        //TODO--Not Done        
        /// <summary>
        /// Gets all shipments by customer. - NOT WORKING
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="name">The name of the customer.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets shipments that are on the way to a specific city.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="cityName">Name of the destination city.</param>
        /// <returns></returns>
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
