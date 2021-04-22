using System.Collections.Generic;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Models;
using DeliverIt13.Services.Models.ParcelDTOs;

namespace DeliverIt13.Services.Contracts
{
    public interface IParcelService
    {
        ParcelCustomerDTO Get(int id);
        List<ParcelSortDTO> GetAllCustomer(UserAuthDTO user);
        List<ParcelEmployeeDTO> GetAll();
        ParcelCreateDTO Create(ParcelCreateDTO parcel);
        void Delete(int id);
        ParcelCreateDTO Update(ParcelCreateDTO parcelDTO);
        List<ParcelEmployeeDTO> GetAllFiltered(ParcelFilterDTO filter);
    }
}