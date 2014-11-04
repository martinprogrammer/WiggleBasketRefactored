using System;
namespace WiggleBasketRefactored.Repositories
{
    interface ICartRepository
    {
        void AddToCart(WiggleBasketRefactored.Models.Products.Product productToAdd);
        string ApplyVoucher(string name);
        string CheckDeleteOfferVouchers();
        string GetCartID(System.Web.HttpContextBase context);
        System.Collections.Generic.IEnumerable<WiggleBasketRefactored.Models.Shopping.Cart> GetCartItems();
        WiggleBasketRefactored.Models.Products.Product GetVoucherByName(string name);
        bool HasThresholdBeenReached(WiggleBasketRefactored.Models.Products.Product voucher);
        void IncreaseNumber(int ID);
        bool IsVoucherCategoryApplicable(bool Throw, WiggleBasketRefactored.Models.Products.Product voucher);
        void ReduceNumber(int ID);
        void RemoveFromCart(int ID);
        decimal ReturnThresholdTotal();
        string ReturnTotal();
    }
}
