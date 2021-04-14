using DeliverIt13.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Contracts
{
    public interface ICityService
    {
        CityDTO Get(int id);

        List<CityDTO> GetAll();
    }
}
