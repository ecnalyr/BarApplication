using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarApp.Models;

namespace BarApp.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        BarAppEntities storeDB = new BarAppEntities();
        const string PromoCode = "FREE";

        //
        // GET: /Checkout/AddressAndPayment

        public ActionResult AddressAndPayment()
        {
            ViewBag.firstName = CustomProfile.GetUserProfile(User.Identity.Name).FirstName;
            CustomProfile profile = CustomProfile.GetUserProfile(User.Identity.Name);
            Order model = new Order
            {
                FirstName = CustomProfile.GetUserProfile(User.Identity.Name).FirstName,
                LastName = profile.LastName,
                Address = profile.Address,
                City = profile.City,
                State = profile.State,
                PostalCode = profile.PostalCode,
                Phone = profile.Phone,
            };
            return View(model);
        }

        //
        // POST: /Checkout/AddressAndPayment

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            var code = RandomCode.Generate();

            order.OrderCode = code;
            order.Active = true;

            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();

                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId, code = order.OrderCode });
                }

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete

        public ActionResult Complete(int id, string code)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                ViewBag.code = code;
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
