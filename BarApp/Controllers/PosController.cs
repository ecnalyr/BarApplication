using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarApp.Models;

namespace BarApp.Controllers
{
    public class PosController : Controller
    {
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
            // search for code in Order table
            // Check if that order is "Active"
            // explain the results
            BarAppEntities orderDB = new BarAppEntities();
            var order = orderDB.Orders.SingleOrDefault(
                o => o.OrderCode == code
                    && o.Active == true);
            if (order == null)
            {
                ViewBag.sample = "null";
            }
            else
            {
                ViewBag.sample = order.Email;
            }
            return View();
        }

    }
}
