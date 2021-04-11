using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DeliverIt13.Data.Enums;

namespace DeliverIt13.Data.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } 
        [Required]
        public string Email { get; set; }
        [Required]
        public UserType Type { get; set; }     
        [Required]
        public string Password { get; set; }
          
    }
}
