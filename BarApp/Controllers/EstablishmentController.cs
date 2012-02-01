using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarApp.Models;

namespace BarApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EstablishmentController : Controller
    {
        BarAppEntities storeDB = new BarAppEntities();
        //
        // GET: /Store/

        public ActionResult Index() // returns an ActionResult (in this case a View) instead of a string like the above options
        {
            // returns a list of all Establishments from the database 
            var establish = storeDB.Establishment.ToList();
            return View(establish);
        }

        public string Location(string city)
        {
            string message = HttpUtility.HtmlEncode("Store.Location, City = " + city); //capitalization of the "City" after the "," is not important // Store.Location, City makes you have to type "/Store/Location?City=Corpus Christi"

            return message;
        }

        // GET: /Store/Establishment
        public ActionResult Establishment(string store) // needs to be an int id to correspond to the id of the database entity
        {
            // Retrieve Establishment and its associated Drinks from database
            var establishModel = storeDB.Establishment.Include("drinks").Single(e => e.name == store);

            return View(establishModel);
            /*var location = new Establishments { name = store };
            return View(location);
            */
        }

        // GET: /Store/Drinks
        public ActionResult Drinks(string drink) // will likely need to be an int id to correspond to the id of the database entity
        {
            // Needs to get a list of all available drinks at an Establishment
            var drinkModel = new Drinks { name = drink };
            return View(drinkModel);
        }

    }
}
