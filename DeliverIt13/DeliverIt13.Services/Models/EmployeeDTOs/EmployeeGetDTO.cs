using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models
{
    public class EmployeeGetDTO
    {
        public EmployeeGetDTO() { }
        public EmployeeGetDTO(Employee employee)
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