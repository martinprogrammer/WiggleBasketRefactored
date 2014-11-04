using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WiggleBasketRefactored.Models;
using WiggleBasketRefactored.Models.Shopping;
using WiggleBasketRefactored.Models.Products;
using WiggleBasketRefactored.Validation;

namespace WiggleBasketRefactored.Repositories
{
    public class CartRepository : WiggleBasketRefactored.Repositories.ICartRepository
    {
        private DBContext myContext = new DBContext();
        private string WiggleCartID { get; set; }
        private const string CartSessionKey = "CartID";
        private string filterOffer = ProductTypeEnum.Offer_Vouchers.ToString();
        private string filterGift = ProductTypeEnum.Gift_Vouchers.ToString();
        private string filterItem = ProductTypeEnum.Items.ToString();

        public static CartRepository GetCart(HttpContextBase context)
        {
            var cart = new CartRepository();
            cart.WiggleCartID = cart.GetCartID(context);
            return cart;
        }

        public void AddToCart(Product productToAdd)
        {
            var existingCartItem = myContext.Carts.SingleOrDefault(p => p.CartID == WiggleCartID && p.ProductID == productToAdd.ID);

            if (existingCartItem == null)
            {
                Cart newCartItem = new Cart();
                newCartItem.CartID = WiggleCartID;
                newCartItem.ProductID = productToAdd.ID;
                newCartItem.Count = 1;
                newCartItem.DateCreated = DateTime.Now;

                myContext.Carts.Add(newCartItem);
                myContext.SaveChanges();
            }
            else
            {
                existingCartItem.Count++;
                myContext.SaveChanges();
            }
        }

        public string GetCartID(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartID = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartID.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public IEnumerable<Models.Shopping.Cart> GetCartItems()
        {
            string filterType = ProductTypeEnum.Offer_Vouchers.ToString();
            return myContext.Carts.Where(cart => cart.CartID == WiggleCartID && cart.Products.Type!=filterType).ToList();
        }
                
        public void IncreaseNumber(int ID)
        {
            var CartItem = myContext.Carts.Single(p => p.CartID == WiggleCartID && p.ProductID == ID);
            if (CartItem != null)
            {
                CartItem.Count += 1;
                myContext.SaveChanges();
            }
        }

        public void ReduceNumber(int ID)
        {
            var CartItem = myContext.Carts.Single(p => p.CartID == WiggleCartID && p.ProductID == ID);
            if (CartItem != null && CartItem.Count > 0)
            {
                CartItem.Count -= 1;
                myContext.SaveChanges();
            }

            if (CartItem.Count == 0)
            {
                RemoveFromCart(ID);
            }
        }

        public void RemoveFromCart(int ID)
        {

            var CartItem = myContext.Carts.Single(p => p.CartID == WiggleCartID && p.ProductID == ID);
            if (CartItem != null)
            {

                myContext.Carts.Remove(CartItem);
                myContext.SaveChanges();
            }
        }

        public decimal ReturnThresholdTotal()
        {
            try
            {
                if (myContext.Carts.Any(p => p.CartID == WiggleCartID && p.Products.Type == filterItem))
                {
                    var total = (from p in myContext.Carts
                             where p.Products.Type == filterItem
                             && p.CartID == WiggleCartID
                             select p.Products.Price * p.Count).Sum();
                    return total;
                }
                else
                {
                    return 0;
                }
                
            }
            catch
            {
                return 0;
            }
        }



        public string ReturnTotal()
        {
            decimal total=0;

            try
            {
                var thresholdTotal = ReturnThresholdTotal();

                if (myContext.Carts.Any(p => p.CartID == WiggleCartID && p.Products.Type == filterOffer))
                {
                    var totalAll = (from p in myContext.Carts
                                 where p.CartID == WiggleCartID && p.Products.Type != filterOffer
                                 select p.Products.Price * p.Count).Sum();

                    total += totalAll;
                }

                var totalOffers = (from p in myContext.Carts
                                   where p.CartID == WiggleCartID && p.Products.Type == filterOffer
                                   select p);

                foreach(Cart p in totalOffers)
                {
                    if (myContext.Carts.Any(d => d.Products.Category == p.Products.Category && d.CartID == WiggleCartID && d.Products.Type == filterItem && thresholdTotal >= p.Products.OfferThreshold))
                    {
                        total -= p.Products.Price;
                    }
                }

                    if (myContext.Carts.Any(p => p.CartID == WiggleCartID && p.Products.Type == filterGift && p.Applied == true))
                    {
                        var totalGifts = (from p in myContext.Carts
                                              where p.CartID == WiggleCartID && p.Products.Type == filterGift && p.Applied == true
                                              select p.Products.Price * p.Count).Sum();

                            total -= totalGifts;
                    }
                
                
                    return "Total: £" + total.ToString();
            }
            
            catch(Exception ex)
            {
                return "There is nothing in the basket";
            }
        }

        public string ApplyVoucher(string name)
        {
            Product voucher = GetVoucherByName(name);

            if(voucher ==null)
            {
                throw new IndexOutOfRangeException("Not a valid voucher");
            }
            
            IRule rule1 = new IsVoucherFirstTimeApplied(voucher, WiggleCartID, 0);

            if (rule1.ExecuteRule())
            {
                if (IsVoucherCategoryApplicable(true, voucher))
                {
                    if (HasThresholdBeenReached(voucher))
                    {
                        try
                        {
                            var cartItem = myContext.Carts.SingleOrDefault(p => p.Products.Name == voucher.Name && p.CartID == WiggleCartID);
                            cartItem.Applied = true;
                            myContext.SaveChanges();
                        }
                        catch
                        {
                            throw new IndexOutOfRangeException("This voucher is not in your cart!");
                        }

                        return "1 x " + voucher.Price + " off " + voucher.Category + " in baskets over " + voucher.OfferThreshold+ ". "+ voucher.Name + " applied";
                    }
                }
            }

            return "Should be unreachable";
           
              
        }

     

        public Product GetVoucherByName(string name)
        {
            string productType = ProductTypeEnum.Items.ToString();

            var voucher = myContext.Products.SingleOrDefault(p => p.Name == name && p.Type != productType);
            return voucher;
        }

        public bool IsVoucherCategoryApplicable(bool Throw, Product voucher)
        {
            if (voucher.Type != filterOffer || voucher.Category =="All")
            {
                return true;
            }
            else
            {
                var category = myContext.Carts.Any(p => p.Products.Type != voucher.Type && p.Products.Category == voucher.Category && p.CartID == WiggleCartID);

                if (category == true)
                {
                    return true;
                }
                else
                {
                    if (Throw)
                    {
                        throw new IndexOutOfRangeException("There are no products in your basket applicable to voucher " + voucher.Name.ToString());
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }


        public bool HasThresholdBeenReached(Product voucher)
        {
            if (voucher.Type == filterGift && ReturnThresholdTotal() >= voucher.Price)
            {
                return true;
            }
            else
            {
                if (voucher.Type == filterOffer && ReturnThresholdTotal()>=voucher.OfferThreshold)
                {
                    return true;
                }
                else
                {
                    throw new IndexOutOfRangeException("You have not reached the spend threshold for voucher " +
                        voucher.Name + ". Spend another £" + (voucher.OfferThreshold - ReturnThresholdTotal()) +
                        " to receive £" + voucher.Price + " discount from your basket total.");
                }

            throw new IndexOutOfRangeException("You need to spend at least " + voucher.Price + " to claim this voucher.");
            }
        }


        public string CheckDeleteOfferVouchers()
        {
            var vouchers = myContext.Carts.Where(p => p.CartID == WiggleCartID && p.Products.Type == filterOffer);
            
            foreach (var voucher in vouchers)
            {
                if (ReturnThresholdTotal() < voucher.Products.OfferThreshold || IsVoucherCategoryApplicable(false, voucher.Products)==false)
                {
                    voucher.Applied = false;
                }
            }

            myContext.SaveChanges();
            return "";

        }
      
    
    }
       
}