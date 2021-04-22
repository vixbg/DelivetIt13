using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverIt13.Data.Enums;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models.UserDTOs;
using DeliverIt13.Web.Helpers;

namespace DeliverIt13.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IAuthHelper authHelper;

        public EmployeesController(IEmployeeService employeeService, IAuthHelper authHelper)
        {
            this.employeeService = employeeService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Gets the specified employee.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="id">Id of the requested employee.</param>
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

                var employee = this.employeeService.Get(id);
                return Ok(employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all employees.
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
                var employee = this.employeeService.GetAll();
                return Ok(employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates the specified employee.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="employee">Employee to be created.</param>
        /// <returns></returns>
        [HttpPost("")]
        public IActionResult Post([FromHeader] string credentials,[FromBody] EmployeeCreateDTO employee)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                this.employeeService.Create(employee);
                return Created("Employee created", employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates the specified employee.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="employee">Employee to be updated.</param>
        /// <returns></returns>
        [HttpPut("")]
        public IActionResult Put([FromHeader] string credentials, [FromBody] EmployeeUpdateDTO employee)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Employee)
                {
                    return Unauthorized(credentials);
                }
                
                var updatedEmployee = this.employeeService.Update(employee);
                return Ok(updatedEmployee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes the specified employee.
        /// </summary>
        /// <param name="credentials">User authentication - employee.</param>
        /// <param name="id">Id of the employee to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromHeader] string credentials, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials);
                if (user.Type != UserType.Customer)
                {
                    return Unauthorized(credentials);
                }

                this.employeeService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
