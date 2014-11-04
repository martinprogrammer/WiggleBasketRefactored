using System;
namespace WiggleBasketRefactored.Repositories
{
    interface IInventoryRepository
    {
        void AddProduct(WiggleBasketRefactored.Models.Products.Product newProduct);
        void EditProduct(WiggleBasketRefactored.Models.Products.Product product);
        WiggleBasketRefactored.Models.Products.Product GetProductByID(int id);
        System.Collections.Generic.IEnumerable<WiggleBasketRefactored.Models.Products.Product> GetProducts();
        bool IsUnique(string name);
        void RemoveProduct(int id);
    }
}
