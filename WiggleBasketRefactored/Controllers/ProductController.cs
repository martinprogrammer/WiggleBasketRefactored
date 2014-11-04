using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiggleBasketRefactored.Models.Products;
using WiggleBasketRefactored.Repositories;

namespace WiggleBasketRefactored.Controllers
{ 
    public class ProductController : Controller
    {
        private InventoryRepository db = new InventoryRepository();

        //
        // GET: /Product/

        public ViewResult Index()
        {
            List<Product> result;
            int? count = (int)db.GetProducts().ToList().Count;
            if (count == null)
                result = new List<Product>();
            else
                result = db.GetProducts().ToList();
            return View(result);
        }

        //
        // GET: /Product/Details/5

        public ViewResult Details(int id)
        {
            Product myProduct = db.GetProductByID(id);
            return View(myProduct);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.IsUnique(newProduct.Name))
                    {
                        db.AddProduct(newProduct);
                        return RedirectToAction("Index");  
                    }
                    else
                    {
                        throw new ArgumentException("the name must be unique");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("NameError", ex.Message);
                }
               
            }

            return View();
        }
        
        //
        // GET: /Product/Edit/5
 
        public ActionResult Edit(int id)
        {
            Product product = db.GetProductByID(id);
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.EditProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //
        // GET: /Product/Delete/5
 
        public ActionResult Delete(int id)
        {
            db.RemoveProduct(id);
            return RedirectToAction("Index");
        }

      
    }
}