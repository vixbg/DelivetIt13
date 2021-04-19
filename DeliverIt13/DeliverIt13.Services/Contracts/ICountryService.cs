using DeliverIt13.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Contracts
{
    public interface ICountryService
    {
        CountryDTO Get(int id);

        List<CountryDTO> GetAll();
    }
}
