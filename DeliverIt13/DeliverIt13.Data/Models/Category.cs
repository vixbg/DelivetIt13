using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT_DemoProject.Models
{
    public class Category
    {
        //can be electronics, clothing, medical, etc
        public int CategoryId { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "City name is not valid!")]
        public string Name { get; set; }
    }
}
