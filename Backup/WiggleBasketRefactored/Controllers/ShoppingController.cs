using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiggleBasketRefactored.Repositories;
using WiggleBasketRefactored.Models.Products;

namespace WiggleBasketRefactored.Controllers
{ 
    public class ShoppingController : Controller
    {
        private ShoppingRepository dbShopping = new ShoppingRepository();
        private InventoryRepository dbProducts = new InventoryRepository();

        //
        // GET: /Shopping/

        public ViewResult Index()
        {
            IEnumerable<Product> offers = dbShopping.GetOffers();
            if (offers.Count() > 0)
            {
                ViewData["Offers"] = offers;
            }
           
            var products = dbShopping.GetProducts();
            return View(products);
        }


        public ActionResult AddToCart(int id)
        {
            var ItemToAdd = dbProducts.GetProductByID(id);
            CartRepository cart = CartRepository.GetCart(this.HttpContext);
            cart.AddToCart(ItemToAdd);
            return RedirectToAction("Index", "Cart");
        }
        

      

      
    }
}