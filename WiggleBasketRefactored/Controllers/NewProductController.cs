using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiggleBasketRefactored.Models.Products;

namespace WiggleBasketRefactored.Controllers
{
    public class NewProductController : BaseController
    {
        private readonly IProductRepository _productRepository;

    
        public NewProductController(IUnitOfWorkManager unitOfWorkManager, IProductRepository productRepository) : base(unitOfWorkManager)
        {
            this._productRepository = productRepository;
        }

        public ActionResult Test()
        {
            using (var unitOfWork = this.UnitOfWorkManager.newUnitOfwork())
            {
                this._productRepository.Add(new Product
                {
                    Name = "Shoe Laces",
                    ImageName = "jumper1.jpg",
                    Category = "tops",
                    Price = 35.50M,
                    OfferThreshold = 25.50M,
                    Type = ProductTypeEnum.Items.ToString()
                });

                unitOfWork.Commit();
            }

            return View();
        }
    }
}