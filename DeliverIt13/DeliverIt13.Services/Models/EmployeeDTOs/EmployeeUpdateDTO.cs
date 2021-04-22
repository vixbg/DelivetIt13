using System;
using System.Collections.Generic;
using System.Text;
using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models.UserDTOs
{
    public class EmployeeUpdateDTO
    {
        public EmployeeUpdateDTO() { }
        public EmployeeUpdateDTO(Employee employee)
        {
            this.EmployeeId = employee.EmployeeId;
            this.UserId = employee.UserId;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.WarehouseId = employee.WarehouseId;
        }

        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int WarehouseId { get; set; }
    }
}
