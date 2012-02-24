using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarApp.Models;
using BarApp.ViewModels;
using System.Web.Security;


namespace BarApp.Controllers
{
    //[Authorize(Roles = "Administrator, Manager")]

    public class PosController : Controller
    {
        private BarAppEntities db = new BarAppEntities();
        //
        // GET: /Pos/

        public ActionResult Index()
        {
            var first = RandomCode.Generate();
            ViewBag.rndm =  first;
            return View();
        }

        //
        // POST: /Pos/

        [HttpPost]
        public ActionResult Index(FormCollection sampleKey)
        {
            string code = sampleKey["sampleCode"]; // Need to check if this key is active
            // search for code in Order table, if code exists make sure that the code is active - if it is active and exists, display user email
            //BarAppEntities orderDB = new BarAppEntities();
            var order = db.Orders.SingleOrDefault(
                o => o.OrderCode == code
                    && o.Active == true);
            if (order == null)
            {
                ViewBag.sample = "The code has either been claimed or is invalid - (check for typos and try again)";
            }
            else
            {
                /*The following code ultimately allows a check of the collected sampleKey against the
                 * current user's "OwnedBar" within their profile.
                 * The intent is to make it so only user who 'owns the bar' will be able to verify a key 
                 * that correlates to a purchase of a drink from their bar.
                 * */
                var orderIdent = db.OrderDetails.SingleOrDefault(
                    p => p.OrderDetailId == order.OrderId);
                var barIdent = db.Drink.SingleOrDefault(
                    q => q.EstablishmentsID == orderIdent.DrinksId);
                var barName = db.Establishment.SingleOrDefault(
                    r => r.EstablishmentsId == barIdent.EstablishmentsID);
                ViewBag.barId = barName.name;
                ViewBag.sample = order.Email;
                var custProfile = CustomProfile.GetUserProfile();
                if (custProfile.OwnedBar != barName.name)
                {
                    ViewBag.msg = "This drink is for another bar";
                }
                else
                {
                    ViewBag.msg = "This drink is for your bar";
                }
            }
            return View();
        }

        //
        // GET: /Pos/ManageProfile

        public ActionResult ManageProfile()
        {
            //var users = Membership.GetAllUsers();
            //return View(users);
            var users = Membership.GetAllUsers();
            var bars = db.Establishment;
            var model = new ChangeProfileViewModel
            {
                Users = users.OfType<MembershipUser>().Select(x => new SelectListItem
                {
                    Value = x.UserName,
                    Text = x.UserName,
                }),
                Bars = bars.OfType<Establishments>().Select(y => new SelectListItem
                {
                    Value = y.name,
                    Text = y.name
                })
            };
            return View(model);
        }


        //
        // POST: /Pos/ManageProfile

        [HttpPost]
        public ActionResult ManageProfile(string selectedUser, string selectedBar)
        {
            CustomProfile profile = CustomProfile.GetUserProfile(selectedUser);
            profile.OwnedBar = selectedBar;
            profile.Save();
            return Content("Thank you for selecting " + selectedUser + selectedBar);
        }
    }
}
