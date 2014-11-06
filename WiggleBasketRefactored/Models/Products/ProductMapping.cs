using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace WiggleBasketRefactored.Models.Products
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            this.HasKey(x => x.ID);
            this.ToTable("Products1");
        }
    }
}