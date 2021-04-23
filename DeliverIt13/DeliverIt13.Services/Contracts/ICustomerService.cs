using DeliverIt13.Services.Models.CustomerDTOs;
using System.Collections.Generic;

namespace DeliverIt13.Services.Contracts
{
    public interface ICustomerService
    {
        CustomerGetDTO Get(int id);

        List<CustomerGetDTO> GetAllBySearch(string search, string searchby);

        List<CustomerGetDTO> GetAll();

        int GetCount();

        CustomerCreateDTO Create(CustomerCreateDTO customer);

        bool Delete(int id);

        CustomerUpdateDTO Update(CustomerUpdateDTO customerDTO);
    }
}
