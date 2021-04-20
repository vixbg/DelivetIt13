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
                var parcel = this.parcelService.Get(id);
                return Ok(parcel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

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
                    // NapProp -> Shipment -> ORderBy -> Shipment.ArrivalDate
                    _ => parcels.OrderBy(p => p.ParcelId)
                };

                if (!string.IsNullOrEmpty(and))
                {
                    orderedParcels = and switch
                    {
                        "weight" => orderedParcels.ThenBy(p => p.Weight),
                        // TODO: NavProp
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

        [HttpPost("filter")]
        public IActionResult GetAllFiltered([FromHeader] string credentials, [FromBody] ParcelFilterDto filter)
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

        [HttpGet("")]
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromHeader] string credentials)
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

        [HttpPut("")]
        public IActionResult Put([FromBody] ParcelCreateDTO parcel,[FromHeader] string credentials)
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
