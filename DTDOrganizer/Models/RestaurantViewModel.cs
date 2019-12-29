using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    public class RestaurantViewModel
    {
        [DisplayName("Restaurant name")]
        public string name { get; set; }
        [DisplayName("Price range")]
        public priceRange priceRange { get; set; }
        public HttpPostedFileBase menuImage { get; set; }
    }
}