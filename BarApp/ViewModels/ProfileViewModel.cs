using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarApp.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string FavoriteDrink { get; set; }
        public string FavoriteBar { get; set; }
    }
}
