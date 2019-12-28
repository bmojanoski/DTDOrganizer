using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    public class RestaurantViewModel
    {
        public string name { get; set; }
        public priceRange priceRange { get; set; }
        public HttpPostedFileBase menuImage { get; set; }
    }
}