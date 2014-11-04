using System;
namespace WiggleBasketRefactored.Repositories
{
    interface IShoppingRepository
    {
        System.Collections.Generic.IEnumerable<WiggleBasketRefactored.Models.Products.Product> GetOffers();
        System.Collections.Generic.IEnumerable<WiggleBasketRefactored.Models.Products.Product> GetProducts();
    }
}
