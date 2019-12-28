using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    public class OrdersModel
    {   
        [Key]
        public int id { get; set; }
        public string restaurantName { get; set; }
        [MaxLength(150)]
        public string order { get; set; }
        public string employee { get; set; }
        public string orderDate { get; set; }
    }
}