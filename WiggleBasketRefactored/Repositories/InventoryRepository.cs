using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiggleBasketRefactored.Models.Products;
using WiggleBasketRefactored.Models;
using System.Data.Entity;

namespace WiggleBasketRefactored.Repositories
{
    public class InventoryRepository : WiggleBasketRefactored.Repositories.IInventoryRepository
    {

        private DBContext db = new DBContext();
        private IEnumerable<Product> myProducts;
      

        public void AddProduct(Product newProduct)
        {
            db.Products.Add(newProduct);
            db.SaveChanges();
        }

        public void RemoveProduct(int id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public Product GetProductByID(int id)
        {
            var product = db.Products.Find(id);
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            return db.Products;
        }


        public void EditProduct(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }


        public bool IsUnique(string name)
        {
            var result = db.Products.Where(p => p.Name == name);
            if (result.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public IEnumerable<string> GetCategories()
        //{
        //    var CategoriesList = new List<string>();
        //    var CategoriesQuery = from cat in db.ProductCategories
        //                          orderby cat.Category
        //                          select cat.Category;
        //    CategoriesList.AddRange(CategoriesQuery.Distinct());

            //var CategoriesList = db.ProductCategories
            //      .ToList() 
            //      .Select(x => new SelectListItem
            //      {
            //          Text = x.Category,
            //          Value = x.ID.ToString()
            //      });
          
        //    return CategoriesList;
        //}

        //public IEnumerable<string> GetTypes()
        //{
        //    var TypesList = new List<string>();

        //    var TypesQuery = from typ in db.ProductTypes
        //                          orderby typ.Type
        //                          select typ.Type;

        //    TypesList.AddRange(TypesQuery.Distinct());

        //    return TypesList;
        //}
    }
}
