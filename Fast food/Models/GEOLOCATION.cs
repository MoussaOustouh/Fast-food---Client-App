using System;
using System.Collections.Generic;
using System.Text;

namespace Fast_food.Models
{
    public partial class GEOLOCATION
    {
        public int id_geolocation { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public byte[] datetime { get; set; }
        public int id_order { get; set; }

        /*public virtual TORDER TORDER { get; set; }*/
    }
}
