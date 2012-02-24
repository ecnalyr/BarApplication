using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BarApp.ViewModels
{
    public class ChangeProfileViewModel
    {
        [Display(Name = "select user")]
        public string SelectedUser { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }

        [Display(Name = "select bar")]
        public string SelectedBar { get; set; }
        public IEnumerable<SelectListItem> Bars { get; set; }
    }
}