using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarApp.Models;


namespace BarApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PromoManagerController : Controller
    {
        private BarAppEntities db = new BarAppEntities();

        //
        // GET: /PromoManager/

        public ViewResult Index()
        {
            // I used the following one time to store Drink1 to Admin's "Favorite Drink" profile spot.
            /*CustomProfile profile = CustomProfile.GetUserProfile();
            profile.FavoriteDrink = "Drink3";
            profile.Save();*/
            
            return View(db.Promotion.ToList());
        }

        //
        // GET: /PromoManager/Details/5

        public ViewResult Details(int id)
        {
            Promotions promotions = db.Promotion.Find(id);
            return View(promotions);
        }

        //
        // GET: /PromoManager/Create

        public ActionResult Create()
        {
            ViewBag.DrinksId = new SelectList(db.Drink, "DrinksId", "name");
            return View();
        } 

        //
        // POST: /PromoManager/Create

        [HttpPost]
        public ActionResult Create(Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                db.Promotion.Add(promotions);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(promotions);
        }
        
        //
        // GET: /PromoManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Promotions promotions = db.Promotion.Find(id);
            return View(promotions);
        }

        //
        // POST: /PromoManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotions);
        }

        //
        // GET: /PromoManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Promotions promotions = db.Promotion.Find(id);
            return View(promotions);
        }

        //
        // POST: /PromoManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Promotions promotions = db.Promotion.Find(id);
            db.Promotion.Remove(promotions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}