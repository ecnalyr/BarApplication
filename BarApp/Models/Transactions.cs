using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarApp.Models
{
    public class Transactions
    {
        public int TransactionsId { get; set; }
        public int purchaserId { get; set; } //Currently only have "person" Ids, no purchaser Ids anywhere
        public int recipientId { get; set; }
        public int establishmentId { get; set; }
        public int drinkId { get; set; }
        public int promotionId { get; set; }

        public virtual Persons purchaser { get; set; } // purchaser and recipient need to reference person Ids'
        public virtual Persons recipient { get; set; }
        public virtual Establishments establishment { get; set; }
        public virtual Drinks drink { get; set; }
        public virtual Promotions promotion { get; set; }
        public virtual DateTime dateTime { get; set; }
    }
}
