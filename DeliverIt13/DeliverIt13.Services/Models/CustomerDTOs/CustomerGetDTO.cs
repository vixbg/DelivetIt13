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
            this.FirstName = customer.FirstName;
            this.LastName = customer.LastName;
            this.Email = customer.User.Email;
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
