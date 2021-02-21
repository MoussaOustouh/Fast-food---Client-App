using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fast_food.Models.MyModels
{
    public class OrderFullInfo : TOrder
    {
        public OrderFullInfo()
        {
            orderVisibility = new OrderVisibility();
        }
        public Client client { get; set; }
        public Delivery_man delivery_man { get; set; }

        public ICollection<Geolocation> geolocations { get; set; }
        public ICollection<Order_content> order_contents { get; set; }

        public float totalPrice { get => (this.order_contents == null) ? 0 : (float)order_contents.Select(oc => oc.price * oc.quantity).Sum(); }

        public int productsQuantity { get => (this.order_contents == null) ? 0 : order_contents.Count(); }


        public OrderVisibility orderVisibility { get; set; }
    }
}