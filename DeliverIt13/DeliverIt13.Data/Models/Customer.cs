using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT_DemoProject.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters long!")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 50 characters long!")]
        public string LastName { get; set; }

        //address?
    }
}
