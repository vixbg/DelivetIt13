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
        /// Gets the specified user.
        /// </summary>
        /// <param name="credentials">User authorization - employee.</param>
        /// <param name="id">Id of the user.</param>
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

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <param name="credentials">User authorization - employee.</param>
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

                return Ok(this.userService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="userDTO">The user.</param>
        /// <param name="credentials">User authorization - employee.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a user - public.
        /// </summary>
        /// <param name="userDTO">The user that will be created.</param>
        /// <returns></returns>
        [HttpPost("public")]
        public IActionResult PostPublic([FromBody] UserCreatePublicDTO userDTO)
        {
            try
            {
                var userId = this.userService.CreatePublic(userDTO);
                return Created("User Created", userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="id">Id of the user.</param>
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

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="userDTO">The user.</param>
        /// <param name="credentials">User authorization - employee.</param>
        /// <returns></returns>
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
