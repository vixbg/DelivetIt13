using DeliverIt13.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Models.CustomerDTOs
{
    public class CustomerGetDTO
    {
        public CustomerGetDTO(Customer customer)
        {
            this.CustomerId = customer.CustomerId;
            this.UserId = customer.UserId;
            this.FirstName = customer.FirstName;
            this.LastName = customer.LastName;
            this.Email = customer.User.Email;
            this.City = customer.City.Name;
            this.Country = customer.City.Country.Name;
            this.CityId = customer.CityId;
            this.Street = customer.Street;
        }

        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
