using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarApp.Models
{
    public class Profiles
    {
        public int ProfilesId { get; set; }
        public int personId { get; set; }
        public string profileComments { get; set; }
        public float balance { get; set; }
        public int transactionId { get; set; }
        public int achievementId { get; set; }
        public int partyId { get; set; }


        public virtual Persons person { get; set; }
        public virtual Transactions transaction { get; set; }
        public virtual Achievements achievement { get; set; }
        public virtual Parties party { get; set; }
    }
}