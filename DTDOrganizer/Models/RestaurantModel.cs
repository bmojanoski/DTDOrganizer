using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    public class RestaurantModel
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public priceRange priceRange { get; set; }
        public string menuImage { get; set; }
    }

    public enum priceRange { 
        cheap, medium, expensive
    }
}