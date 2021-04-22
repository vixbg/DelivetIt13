using System;
using System.Linq;
using DeliverIt13.Data.Enums;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using DeliverIt13.Services.Models.ParcelDTOs;
using DeliverIt13.Services.Models.UserDTOs;
using DeliverIt13.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthHelper authHelper;

        public UsersController(IUserService userService, IAuthHelper authHelper)
        {
            
            this.userService = userService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Gets the specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="id">The identifier.</param>
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
                var userEntity = this.userService.Get(id);
                return Ok(userEntity);
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

                return Ok(this.userService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("")]
        public IActionResult Post([FromBody] UserCreateDTO userDTO, [FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                this.userService.Create(userDTO);
                return Created("User Created", userDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("public")]
        public IActionResult PostPublic([FromBody] UserCreatePublicDTO userDTO, [FromHeader] string credentials)
        {
            try
            {
                this.userService.CreatePublic(userDTO);
                return Created("User Created", userDTO);
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
                this.userService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("")]
        public IActionResult Put([FromBody] UserUpdateDTO userDTO,[FromHeader] string credentials)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                var updatedUser = this.userService.Update(userDTO);
                return Ok(updatedUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
