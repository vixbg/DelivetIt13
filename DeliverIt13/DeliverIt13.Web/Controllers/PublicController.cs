using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicController : ControllerBase
    {
        private readonly IPublicService publicService;

        public PublicController(IPublicService publicService)
        {
            this.publicService = publicService;
        }


        [HttpGet("warehouses")]
        public IActionResult Get()
        {
            var warehouses = this.publicService.GetWarehouses();
            return Ok(warehouses);
        }

        [HttpGet("customercount")]
        public IActionResult Get([FromQuery] string customers)
        {
            var customersCount = this.publicService.GetCustCount();
            return Ok(customersCount);
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] UserPublicDTO user)
        {
            if (user == null)
            {
                return this.BadRequest();
            }

            this.publicService.Register(user);
            return this.Created("User Created", user);
        }
    }
}