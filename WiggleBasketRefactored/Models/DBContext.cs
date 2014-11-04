using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using WiggleBasketRefactored.Models.Products;
using WiggleBasketRefactored.Models.Shopping;

namespace WiggleBasketRefactored.Models
{
    public class DBContext : DbContext
    {

        public DBContext()
            : base("Entities")
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductSpecific> ProductSpecifics { get; set; }

        public ObjectContext ObjectContext()
        {
            return ((IObjectContextAdapter)this).ObjectContext;
        }


    }
}
