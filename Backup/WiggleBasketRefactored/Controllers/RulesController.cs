using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiggleBasketRefactored.Models.Products;
using WiggleBasketRefactored.Models;

namespace WiggleBasketRefactored.Controllers
{ 
    public class RulesController : Controller
    {
        private DBContext db = new DBContext();

        //
        // GET: /Rules/

        public ViewResult Index()
        {
            return View(db.ProductSpecifics.ToList());
        }

        //
        // GET: /Rules/Details/5

        public ViewResult Details(int id)
        {
            ProductSpecific productspecific = db.ProductSpecifics.Find(id);
            return View(productspecific);
        }

        //
        // GET: /Rules/Create

        public ActionResult Create()
        {
            ViewData["id"] = Url.RequestContext.RouteData.Values["id"];
            
            return View();
        } 

        //
        // POST: /Rules/Create

        [HttpPost]
        public ActionResult Create(ProductSpecific productspecific)
        {
            if (ModelState.IsValid)
            {
                db.ProductSpecifics.Add(productspecific);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(productspecific);
        }
        
        //
        // GET: /Rules/Edit/5
 
        public ActionResult Edit(int id)
        {
            ProductSpecific productspecific = db.ProductSpecifics.Find(id);
            return View(productspecific);
        }

        //
        // POST: /Rules/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductSpecific productspecific)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productspecific).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productspecific);
        }

        //
        // GET: /Rules/Delete/5
 
        public ActionResult Delete(int id)
        {
            ProductSpecific productspecific = db.ProductSpecifics.Find(id);
            return View(productspecific);
        }

        //
        // POST: /Rules/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ProductSpecific productspecific = db.ProductSpecifics.Find(id);
            db.ProductSpecifics.Remove(productspecific);
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