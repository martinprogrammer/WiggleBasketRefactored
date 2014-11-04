using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WiggleBasketRefactored.Models.Products;
using WiggleBasketRefactored.Models.Shopping;
using WiggleBasketRefactored.Models;

namespace WiggleBasketRefactored.Repositories
{
    public class ShoppingRepository : WiggleBasketRefactored.Repositories.IShoppingRepository
    {
        private DBContext myContext = new DBContext();
        
        public IEnumerable<Product> GetProducts()
        {
            var result = myContext.Products.Where(p => p.Type != "Offer_Vouchers").AsQueryable();
           

            return result;
        }

        public IEnumerable<Product> GetOffers()
        {
            string filterCategory = ProductTypeEnum.Offer_Vouchers.ToString();
            var result = from p in myContext.Products
                         where p.Type == filterCategory
                         select p;
            
            
            return result;
        }



    }
}