using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    public class FoodViewModel
    {
        public List<RestaurantModel> restaurants { get; set; }
        public List<OrdersModel> dailyOrders { get; set; }
    }
}