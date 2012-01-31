using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarApp.Models
{
    public class Promotions
    {
        public int PromotionsId { get; set; }
        public string name { get; set; }
        public float discount { get; set; }
        public int? EstablishmentsId { get; set; }
        public int? DrinksId { get; set; }
        public string description { get; set; }

        public virtual Establishments establishment { get; set; }
        public virtual Drinks drinks { get; set; }
    }
}
