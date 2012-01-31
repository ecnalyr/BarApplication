using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarApp.Models
{
    public class Parties
    {
        public int PartiesId { get; set; }
        public string name { get; set; }
        public int promoId { get; set; }
        public int establishmentId { get; set; }

        public virtual Promotions promo { get; set; }
        public virtual DateTime dateTime { get; set; }
        public virtual Establishments location { get; set; }
    }
}
