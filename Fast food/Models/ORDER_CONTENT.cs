using System;
using System.Collections.Generic;
using System.Text;

namespace Fast_food.Models
{
    public partial class ORDER_CONTENT
    {
        public int quantity { get; set; }
        public decimal price { get; set; }
        public int id_order { get; set; }
        public int id_product { get; set; }

        /*public virtual TORDER TORDER { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }*/
    }
}
