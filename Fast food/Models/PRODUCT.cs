using System;
using System.Collections.Generic;
using System.Text;

namespace Fast_food.Models
{
    class PRODUCT
    {
        public PRODUCT()
        {
            /*this.ORDER_CONTENT = new HashSet<ORDER_CONTENT>();*/
        }

        public int id_product { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public decimal price { get; set; }
        public byte available { get; set; }
        public int id_admin { get; set; }
        public string picture { get; set; }

        /*public virtual ADMIN ADMIN { get; set; }

        public virtual ICollection<ORDER_CONTENT> ORDER_CONTENT { get; set; }*/
    }
}

