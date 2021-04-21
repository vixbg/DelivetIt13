using DeliverIt13.Services.Models.CustomerDTOs;
using System.Collections.Generic;

namespace DeliverIt13.Services.Contracts
{
    public interface ICustomerService
    {
        CustomerGetDTO Get(int id);

        List<CustomerGetDTO> GetByEmail(string email);

        List<CustomerGetDTO> GetByFirstName(string firstName);

        List<CustomerGetDTO> GetByLastName(string lastName);

        List<CustomerGetDTO> GetAllFiltered(CustomerFilterDTO customer);

        List<CustomerGetDTO> GetAll();

        int GetCount();

        CustomerCreateDTO Create(CustomerCreateDTO customer);

        bool Delete(int id);

        CustomerUpdateDTO Update(int id, CustomerUpdateDTO customerDTO);
    }
}
