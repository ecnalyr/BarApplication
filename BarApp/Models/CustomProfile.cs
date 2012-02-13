using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Profile;

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
    }
}