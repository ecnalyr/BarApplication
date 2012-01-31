using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarApp.Models
{
    public class Establishments
    {
        public int EstablishmentsId { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string state { get; set; }
        public string phone { get; set; }
        public string comments { get; set; }
        public string logoImage { get; set; }
        public List<Drinks> drinks { get; set; } // behaves like public list<album> albums while sitting in the Genre class linking to the Album class
    }
}