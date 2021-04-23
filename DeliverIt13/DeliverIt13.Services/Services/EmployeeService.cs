using System;
using System.Collections.Generic;
using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using System.Linq;
using DeliverIt13.Services.Models.ParcelDTOs;
using DeliverIt13.Services.Models.UserDTOs;
using Microsoft.EntityFrameworkCore;

namespace DeliverIt13.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly DeliverItContext dbContext;
		public EmployeeService(DeliverItContext dbContext)
		{
			this.dbContext = dbContext;
		}

        public EmployeeGetDTO Get(int id)
        {
            var user = this.dbContext.Employees.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                throw new Exception("No Employee found with this ID.");
            }
            var newDTO = new EmployeeGetDTO(user);
            return newDTO;
        }

        public List<EmployeeGetDTO> GetAll()
        {
            var employees = this.dbContext
                .Employees
                .Include(e=>e.User)
                .Include(e=>e.Warehouse)
                .ToList();
            if (employees.Count == 0)
            {
                throw new Exception("No employees found.");
            }
            var userDTOs = new List<EmployeeGetDTO>();
            foreach (var user in employees)
            {
                var newDTO = new EmployeeGetDTO(user);
                userDTOs.Add(newDTO);
            }

            return userDTOs;
        }
        public EmployeeCreateDTO Create(EmployeeCreateDTO employee)
        {
            if (employee == null)
            {
                throw new Exception("Input Employee is Empty or Null.");
            }

            var newEmployee = new Employee();
            newEmployee.UserId = employee.UserId;
            newEmployee.FirstName = employee.FirstName;
            newEmployee.LastName = employee.LastName;
            newEmployee.WarehouseId = employee.WarehouseId;

            this.dbContext.Employees.Add(newEmployee);
            this.dbContext.SaveChanges();

            return employee;

        }

        public bool Delete(int id)
        {
            var employee = this.dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                throw new Exception("No employee found with this ID.");
            }

            this.dbContext.Employees.Remove(employee);
            this.dbContext.SaveChanges();

            return true;
        }

        public EmployeeUpdateDTO Update(EmployeeUpdateDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                throw new Exception("Input User is Empty or Null.");
            }
            var employee = this.dbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeDTO.EmployeeId);

            if (employee == null)
            {
                throw new Exception("No User found with this ID.");
            }

            employee.EmployeeId = employeeDTO.EmployeeId;
            employee.UserId = employeeDTO.UserId;
            employee.WarehouseId = employeeDTO.WarehouseId;
            employee.FirstName = employeeDTO.FirstName;
            employee.LastName = employeeDTO.LastName;
            this.dbContext.SaveChanges();

            return employeeDTO;
        }
        
    }

}
