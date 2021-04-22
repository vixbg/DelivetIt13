using System;
using System.Linq;
using DeliverIt13.Data.Enums;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using DeliverIt13.Services.Models.ParcelDTOs;
using DeliverIt13.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelService parcelService;
        private readonly IAuthHelper authHelper;

        public ParcelController(IParcelService parcelService, IAuthHelper authHelper)
        {
            this.parcelService = parcelService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Gets the specified parcel.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="id">Id of the parcel</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string credentials,int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                var parcel = this.parcelService.Get(id);
                return Ok(parcel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all parcels with sorting.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="sort">Sort by - weight, date, category, warehouse, shipment - can be left blank.</param>
        /// <param name="and">Second sort parameter - and by weight, date, category, warehouse, shipment - can be left blank.</param>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult GetAll([FromHeader] string credentials, [FromQuery] string sort, [FromQuery] string and)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                var parcels = this.parcelService.GetAll();
                var orderedParcels = sort switch
                {
                    "weight" => parcels.OrderBy(p => p.Weight),
                    "date" => parcels.OrderBy(p => p.Shipment.ArrivalDate),
                    "category" => parcels.OrderBy(p => p.Category),
                    "warehouse" => parcels.OrderBy(p => p.Warehouse.City.Name),
                    "shipment" => parcels.OrderBy(p => p.Shipment.Status),
                    _ => parcels.OrderBy(p => p.ParcelId)
                };

                if (!string.IsNullOrEmpty(and))
                {
                    orderedParcels = and switch
                    {
                        "weight" => orderedParcels.ThenBy(p => p.Weight),
                        "date" => orderedParcels.ThenBy(p => p.Shipment.ArrivalDate),
                        "category" => orderedParcels.ThenBy(p => p.Category),
                        "warehouse" => orderedParcels.ThenBy(p => p.Warehouse.City.Name),
                        "shipment" => orderedParcels.ThenBy(p => p.Shipment.Status),
                        _ => orderedParcels.ThenBy(p => p.ParcelId)
                    };
                }
                
                return Ok(orderedParcels);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Gets all parcels, filtered.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="filter">Filter by weight, customer, warehouse, category - can be left blank.</param>
        /// <returns></returns>
        [HttpPost("filter")]
        public IActionResult GetAllFiltered([FromHeader] string credentials, [FromBody] ParcelFilterDTO filter)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                var parcels = this.parcelService.GetAllFiltered(filter);
                return Ok(parcels);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all parcels for specific customer.
        /// </summary>
        /// <param name="credentials">User authentication - customer.</param>
        /// <returns></returns>
        [HttpGet("byCustomer")]
        public IActionResult GetAllCustomer([FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Customer)
                {
                    return Unauthorized(credentials);
                }
                
                var parcels = this.parcelService.GetAllCustomer(user);

                return Ok(parcels);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates the specified parcel.
        /// </summary>
        /// <param name="parcel">The parcel that will be created.</param>
        /// <param name="credentials">User authentication - employee.</param>
        /// <returns></returns>
        [HttpPost("")]
        public IActionResult Post([FromBody] ParcelCreateDTO parcel, [FromHeader] string credentials)
        {

            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                this.parcelService.Create(parcel);
                return Created("Parcel Created", parcel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Deletes the specified parcel.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="id">Id of the parcel.</param>
        /// <returns></returns>
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
                this.parcelService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Updates the specified parcel.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="parcel">The parcel that will be updated.</param>
        /// <returns></returns>
        [HttpPut("")]
        public IActionResult Put([FromHeader] string credentials, [FromBody] ParcelCreateDTO parcel)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                var updatedParcel = this.parcelService.Update(parcel);
                return Ok(updatedParcel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
