using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using Facebook.Web;

namespace BarApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ShotCaller!";

            return View();
        }

        [FacebookAuthorize(LoginUrl = "/Account/Login")]
        public ActionResult FBProfile()
        {
            var client = new FacebookWebClient();

            dynamic me = client.Get("me");
            ViewBag.Name = me.name;
            ViewBag.Id = me.id;
            ViewBag.Email = me.email;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
