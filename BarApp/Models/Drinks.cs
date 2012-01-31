using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarApp.Models
{
    public class Drinks
    {
        public int DrinksId { get; set; }
        public int EstablishmentsID { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string image { get; set; }

        public virtual Establishments establishment { get; set; }
    }
}
