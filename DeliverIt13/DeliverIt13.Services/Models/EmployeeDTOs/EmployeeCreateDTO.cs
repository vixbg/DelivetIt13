using System;
using System.Collections.Generic;
using System.Text;
using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models.UserDTOs
{
    public class EmployeeCreateDTO
    {
        public EmployeeCreateDTO() { }
        public EmployeeCreateDTO(Employee employee)
        {
            this.UserId = employee.UserId;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.WarehouseId = employee.WarehouseId;
        }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int WarehouseId { get; set; }
    }
}
