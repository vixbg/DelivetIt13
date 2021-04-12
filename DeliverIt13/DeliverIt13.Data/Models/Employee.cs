using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverIt13.Data.Models
{
    public class Employee
    {
        [Key]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required, StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters long!")]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 50 characters long!")]
        public string LastName { get; set; }

        public Guid WarehouseId { get; set; }
        [ForeignKey(nameof(WarehouseId))]
        public Warehouse Warehouse { get; set; }
    }
}
