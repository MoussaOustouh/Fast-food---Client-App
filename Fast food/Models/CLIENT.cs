using System;
using System.Collections.Generic;
using System.Text;

namespace Fast_food.Models
{
    public class CLIENT
    {
        public CLIENT()
        {
            /*this.ORDERs = new HashSet<ORDER>();*/
        }

        public int id_client { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string photo { get; set; }
        public string confirmation_code { get; set; }

        /*public virtual ICollection<ORDER> ORDERs { get; set; }*/
    }
}

