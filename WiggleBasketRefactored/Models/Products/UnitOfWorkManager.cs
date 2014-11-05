using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WiggleBasketRefactored.Models.Products
{
     public interface IUnitOfWorkManager : IDisposable
     {
         IUnitOfWork newUnitOfwork();
     }

    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        public UnitOfWorkManager(IWebDbContext context)
        {
            Database.SetInitializer<WebDbContext>(null);
            this._context = context as WebDbContext;
        }
       
        public void Dispose()
        {
            if (this._isDisposed == false)
            {
                this._context.Dispose();
                this._isDisposed = true;
            }
        }

        public IUnitOfWork newUnitOfwork()
        {
            return new UnitOfWork(this._context);
        }

        private bool _isDisposed;
        private readonly WebDbContext _context;
    }
}