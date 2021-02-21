using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fast_food.Models.MyModels
{
    public class OrderVisibility
    {
        public bool newOrders { get; set; }
        public bool showOrdersDateTime { get; set; }
        public bool showDeliveryMen { get; set; }
        public bool ordersDelivred { get; set; }
    }
}