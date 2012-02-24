using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Profile;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarApp.Models
{
    public class CustomProfile : ProfileBase
    {
        public static CustomProfile GetUserProfile(string username)
        {
            return Create(username) as CustomProfile;
        }
        public static CustomProfile GetUserProfile()
        {
            return Create(Membership.GetUser().UserName) as CustomProfile;
        }

        [SettingsAllowAnonymous(false)]
        public string Description
        {
            get { return base["Description"] as string; }
            set { base["Description"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        public string Location
        {
            get { return base["Location"] as string; }
            set { base["Location"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        public string FavoriteDrink
        {
            get { return base["FavoriteDrink"] as string; }
            set { base["FavoriteDrink"] = value;  }
        }

        [SettingsAllowAnonymous(false)]
        public string FavoriteBar
        {
            get { return base["FavoriteBar"] as string; }
            set { base["FavoriteBar"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName
        {
            get { return base["FirstName"] as string; }
            set { base["FirstName"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName
        {
            get { return base["LastName"] as string; }
            set { base["LastName"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public string Address
        {
            get { return base["Address"] as string; }
            set { base["Address"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City
        {
            get { return base["City"] as string; }
            set { base["City"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        [Required(ErrorMessage = "State is required")]
        [StringLength(40)]
        public string State
        {
            get { return base["State"] as string; }
            set { base["State"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        [Required(ErrorMessage = "Postal Code is required")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode
        {
            get { return base["PostalCode"] as string; }
            set { base["PostalCode"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        [Required(ErrorMessage = "Phone is required")]
        [StringLength(24)]
        public string Phone
        {
            get { return base["Phone"] as string; }
            set { base["Phone"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        public string OwnedBar
        {
            get { return base["OwnedBar"] as string; }
            set { base["OwnedBar"] = value; }
        }
    }
}