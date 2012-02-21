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

    }
}
