using DeliverIt13.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Models.CustomerDTOs
{
    public class CustomerUpdateDTO
    {
        public CustomerUpdateDTO()
        {

        }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CityId { get; set; }
        public string Street { get; set; }

    }
}
