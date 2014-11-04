using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiggleBasketRefactored.Models.Shopping;
using WiggleBasketRefactored.Models;
using WiggleBasketRefactored.Repositories;

namespace WiggleBasketRefactored.Controllers
{ 
    public class CartController : Controller
    {

        //
        // GET: /Cart/

        public ViewResult Index()
        {
             CartRepository db = CartRepository.GetCart(this.HttpContext);
             var result = db.GetCartItems();
             db.CheckDeleteOfferVouchers();
             ViewBag.Total = db.ReturnTotal();
             return View(result);
        }

        //
        // POST: /Cart/Delete/5

        [HttpGet, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CartRepository db = CartRepository.GetCart(this.HttpContext);
            db.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public ActionResult IncreaseNumber(int id)
        {
            CartRepository db = CartRepository.GetCart(this.HttpContext);
            db.IncreaseNumber(id);
            return RedirectToAction("Index");
        }

        public ActionResult ReduceNumber(int id)
        {
            CartRepository db = CartRepository.GetCart(this.HttpContext);
            db.ReduceNumber(id);
            return RedirectToAction("Index");
        }

        public ActionResult ApplyVoucher()
        {
            string voucher = Request["VoucherName"];
            CartRepository db = CartRepository.GetCart(this.HttpContext);
            
            try
            {
                if (String.IsNullOrEmpty(voucher) || String.IsNullOrWhiteSpace(voucher))
                {
                    TempData["VoucherValidationMessage"] = "Please type in a valid voucher name";
                }
                else
                {
                    string validationMessage =db.ApplyVoucher(voucher);
                    TempData["VoucherValidationMessage"] = validationMessage;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("NameError", ex.Message);
                TempData["VoucherValidationMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
            
        }

     
    }
}