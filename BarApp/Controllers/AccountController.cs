using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using BarApp.Models;
using System.Data;
using System.Data.Entity;
using BarApp.ViewModels;
using AutoMapper;

namespace BarApp.Controllers
{
    public class AccountController : Controller
    {
        private BarAppEntities db = new BarAppEntities();

        private void MigrateShoppingCart(string UserName)
        {
            // Associate shopping cart items with logged-in user
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.MigrateCart(UserName);
            Session[ShoppingCart.CartSessionKey] = UserName;
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    MigrateShoppingCart(model.UserName);

                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    MigrateShoppingCart(model.UserName);

                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        //
        // GET: /Account/Profile/Profile

        public ActionResult Profile()
        {
            return View();
        }

        //
        // GET: /Account/ChangeProfile

        [Authorize]
        public ActionResult ChangeProfile()
        {
            /*ViewBag.favDrink = CustomProfile.GetUserProfile(User.Identity.Name).FavoriteDrink; // May want to replace this viewBag with a ViewModel that handles all of our profile features.
            ViewBag.favBar = CustomProfile.GetUserProfile(User.Identity.Name).FavoriteBar;
            ViewBag.firstName = CustomProfile.GetUserProfile(User.Identity.Name).FirstName;
            ViewBag.lastName = CustomProfile.GetUserProfile(User.Identity.Name).LastName;
            ViewBag.address = CustomProfile.GetUserProfile(User.Identity.Name).Address;
            ViewBag.city = CustomProfile.GetUserProfile(User.Identity.Name).City;
            ViewBag.state = CustomProfile.GetUserProfile(User.Identity.Name).State;
            ViewBag.postalCode = CustomProfile.GetUserProfile(User.Identity.Name).PostalCode;
            ViewBag.phone = CustomProfile.GetUserProfile(User.Identity.Name).Phone;*/

            CustomProfile profile = CustomProfile.GetUserProfile(User.Identity.Name);
            ProfileViewModel model = new ProfileViewModel
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Address = profile.Address,
                City = profile.City,
                State = profile.State,
                PostalCode = profile.PostalCode,
                Phone = profile.Phone,
                FavoriteDrink = profile.FavoriteDrink,
                FavoriteBar = profile.FavoriteBar
            };
            return View(model);
        }

        //
        // POST: /Account/ChangeProfile

        [Authorize]
        [HttpPost]
        public ActionResult ChangeProfile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // there were validation errors => redisplay the view
                return View(model);
            }

            // validation succeeded => process the results
            CustomProfile profile = CustomProfile.GetUserProfile();
            profile.FirstName = model.FirstName;
            profile.LastName = model.LastName;
            profile.Address = model.Address;
            profile.City = model.City;
            profile.State = model.State;
            profile.PostalCode = model.PostalCode;
            profile.Phone = model.Phone;
            profile.FavoriteDrink = model.FavoriteDrink;
            profile.FavoriteBar = model.FavoriteBar;
            profile.Save();
            return RedirectToAction("Profile");
        }

        /*public ActionResult ChangeProfile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // there were validation errors => redisplay the view
                return View(model);
            }

            // validation succeeded => process the results
            CustomProfile profile = CustomProfile.GetUserProfile();
            Mapper.Map<ProfileViewModel, CustomProfile>(model, profile);
            profile.Save();
            return RedirectToAction("Profile");
        }*/
        /*public ActionResult ChangeProfile( FormCollection favorites )
        {
            //@html.textbox works great if we want user to type in name of drink.  I want user to be able to select a drink from dropdowns of establishment > specific drink.
            CustomProfile profile = CustomProfile.GetUserProfile();
            profile.FirstName = favorites["FirstName"];
            profile.LastName = favorites["LastName"];
            profile.Address = favorites["Address"];
            profile.City = favorites["City"];
            profile.State = favorites["State"];
            profile.PostalCode = favorites["PostalCode"];
            profile.Phone = favorites["Phone"];
            profile.FavoriteDrink = favorites["FavoriteDrink"];
            profile.FavoriteBar = favorites["FavoriteBar"];
            profile.Save();
            return RedirectToAction("Profile");
        }*/

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
