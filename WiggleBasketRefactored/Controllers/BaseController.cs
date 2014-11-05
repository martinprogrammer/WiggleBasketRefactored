using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiggleBasketRefactored.Models.Products;

namespace WiggleBasketRefactored.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWorkManager UnitOfWorkManager;

        public BaseController(IUnitOfWorkManager unitOfWorkManager)
        {
            UnitOfWorkManager = unitOfWorkManager;
        }
    }
}