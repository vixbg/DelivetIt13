using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverIt13.Data.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required, StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters long!")]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 50 characters long!")]
        public string LastName { get; set; }
        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Street name must be between 3 and 100 characters long!")]
        public string Street { get; set; }
    }
}
