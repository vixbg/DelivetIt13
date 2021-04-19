using System;
using System.ComponentModel.DataAnnotations;
using DeliverIt13.Data.Enums;


namespace DeliverIt13.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } 
        [Required]
        public string Email { get; set; }
        [Required]
        public UserType Type { get; set; }     
        [Required]
        public string Password { get; set; }
          
    }
}
