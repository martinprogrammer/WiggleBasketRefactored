using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WiggleBasketRefactored.Models.Products
{
    public interface IWebDbContext : IDisposable
    {
    }

    public class WebDbContext : DBContext, IWebDbContext
    {
      

        public WebDbContext()
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
