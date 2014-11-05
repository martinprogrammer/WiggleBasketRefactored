using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;
using System.Data.Entity;

namespace WiggleBasketRefactored.Models.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product Add(Product item);
        void Update(Product item);
        void Delete(Product item);
        Product Get(int id);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly WebDbContext _context;

        public ProductRepository(IWebDbContext context)
        {
            this._context = context as WebDbContext;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Product Add(Product item)
        {
            _context.Products.Add(item);
            return item;
        }

        public void Update(Product item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Product item)
        {
            _context.Entry(item).State = EntityState.Deleted;
        }

        public Product Get(int id)
        {
            return _context.Products.Where(p => p.ID==id).SingleOrDefault();
        }
    }
}