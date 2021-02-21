using System;
using System.Collections.Generic;
using System.Text;

namespace Fast_food.Models
{
    public partial class TORDER
    {
        public TORDER()
        {
            /*this.GEOLOCATIONs = new HashSet<GEOLOCATION>();
            this.ORDER_CONTENT = new HashSet<ORDER_CONTENT>();*/
        }

        public int id_order { get; set; }
        public string order_state { get; set; }
        public Nullable<double> latitude { get; set; }
        public Nullable<double> longitude { get; set; }
        public Nullable<System.DateTime> order_datetime { get; set; }
        public string order_code { get; set; }
        public Nullable<byte> delivery_state { get; set; }
        public Nullable<System.DateTime> received_datetime { get; set; }
        public Nullable<int> id_delivery_man { get; set; }
        public int id_client { get; set; }

        /*public virtual CLIENT CLIENT { get; set; }
        public virtual DELIVERY_MAN DELIVERY_MAN { get; set; }

        public virtual ICollection<GEOLOCATION> GEOLOCATIONs { get; set; }

        public virtual ICollection<ORDER_CONTENT> ORDER_CONTENT { get; set; }*/
    }
}