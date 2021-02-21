using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fast_food.MyHelpers
{
    public class OrderStates
    {

        //hado lli ansift l server 7it homa lli f order_state f DB
        public static string choosing_food = "Choosing food";
        public static string order_to_deliver = "Order to deliver";
        public static string order_on_the_way = "The order is on the way";
        public static string order_has_been_delivered = "The order has been delivered";

        public static List<string> server_states_list = new List<string> {
            choosing_food,
            order_to_deliver, 
            order_on_the_way, 
            order_has_been_delivered
        };


        // hado lli ghadi n afficher l client
        public static string app_New_orders = "New orders";
        public static string app_Orders_ready_to_deliver = "Orders ready to deliver";
        public static string app_Orders_on_the_way = "Orders on the way";
        public static string app_Delivered_orders = "Delivered orders";

        public static List<string> app_states_list = new List<string>{
            app_New_orders, 
            app_Orders_ready_to_deliver, 
            app_Orders_on_the_way, 
            app_Delivered_orders
        };

        /*
        Orders states : 
            - Choosing food
            - Order to deliver
            - The order is on the way
            - The order has been delivered
         */
    }
}