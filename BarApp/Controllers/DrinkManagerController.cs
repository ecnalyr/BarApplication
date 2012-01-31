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
    //[Authorize(Roles = "Administrator")]
    public class DrinkManagerController : Controller
    {
        private BarAppEntities db = new BarAppEntities();

        //
        // GET: /StoreManager/

        public ViewResult Index()
        {
            return View(db.Drink.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ViewResult Details(int id)
        {
            Drinks drinks = db.Drink.Find(id);
            return View(drinks);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            ViewBag.EstablishmentsId = new SelectList(db.Establishment, "EstablishmentsId", "name");
            return View();
        } 

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Drinks drinks)
        {
            if (ModelState.IsValid)
            {
                db.Drink.Add(drinks);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(drinks);
        }
        
        //
        // GET: /StoreManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Drinks drinks = db.Drink.Find(id);
            ViewBag.EstablishmentsId = new SelectList(db.Establishment, "EstablishmentsId", "name", drinks.EstablishmentsID); // sends necessary info to populate dropdown in the view, sets appropriate default establishment
            return View(drinks);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Drinks drinks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drinks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drinks);
        }

        //
        // GET: /StoreManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Drinks drinks = db.Drink.Find(id);
            return View(drinks);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Drinks drinks = db.Drink.Find(id);
            db.Drink.Remove(drinks);
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