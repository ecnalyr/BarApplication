using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarApp.Models
{
    public class Persons
    {
        public int PersonsId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
    }
}