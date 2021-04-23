using System.Collections.Generic;
using DeliverIt13.Services.Models;
using DeliverIt13.Services.Models.UserDTOs;

namespace DeliverIt13.Services.Contracts
{
    public interface IEmployeeService
    {
        EmployeeGetDTO Get(int id);
        List<EmployeeGetDTO> GetAll();
        EmployeeCreateDTO Create(EmployeeCreateDTO employee);
        EmployeeUpdateDTO Update(EmployeeUpdateDTO employeeDTO);
        bool Delete(int id);
    }
}