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
        public int CityId { get; set; }
        public City City { get; set; }
        public string Street { get; set; }

        //user table

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
