using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Models.CustomerDTOs
{
    public class CustomerCreateDTO
    {
        public CustomerCreateDTO()
        {

        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CityId { get; set; }
        public string Street { get; set; }
    }
}
