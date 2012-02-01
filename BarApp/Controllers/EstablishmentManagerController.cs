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
    public class EstablishmentManagerController : Controller
    {
        private BarAppEntities db = new BarAppEntities();

        //
        // GET: /EstablishmentManager/

        public ViewResult Index()
        {
            return View(db.Establishment.ToList());
        }

        //
        // GET: /EstablishmentManager/Details/5

        public ViewResult Details(int id)
        {
            Establishments establishments = db.Establishment.Find(id);
            return View(establishments);
        }

        //
        // GET: /EstablishmentManager/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /EstablishmentManager/Create

        [HttpPost]
        public ActionResult Create(Establishments establishments)
        {
            if (ModelState.IsValid)
            {
                db.Establishment.Add(establishments);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(establishments);
        }
        
        //
        // GET: /EstablishmentManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Establishments establishments = db.Establishment.Find(id);
            return View(establishments);
        }

        //
        // POST: /EstablishmentManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Establishments establishments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(establishments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(establishments);
        }

        //
        // GET: /EstablishmentManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Establishments establishments = db.Establishment.Find(id);
            return View(establishments);
        }

        //
        // POST: /EstablishmentManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Establishments establishments = db.Establishment.Find(id);
            db.Establishment.Remove(establishments);
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