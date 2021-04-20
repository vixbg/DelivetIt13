using System.Collections.Generic;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Models;

namespace DeliverIt13.Services.Contracts
{
    public interface IPublicService
    {
        CustomerPublicDTO GetCustCount();
        List<WarehouseGetDTO> GetWarehouses();
        UserPublicDTO Register(UserPublicDTO user);

    }
}