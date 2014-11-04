using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WiggleBasketRefactored.Models.Products;

namespace WiggleBasketRefactored.Validation
{
    public class IsVoucherFirstTimeApplied : Rule
    {

        public IsVoucherFirstTimeApplied(Product voucher, string cntxtID, decimal value)
        {
            myContext = new Models.DBContext();
            ruleName = "IsVoucherFirstTimeApplied";
            ruleDescription = "A voucher can only be applied once";
            ruleValue = value;
            productToValidate = voucher;
            errorMessage = "You have already applied this voucher";
            contextID = cntxtID;
        }


        public override bool ExecuteRule()
        {
            if (productToValidate.Type == ProductTypeEnum.Items.ToString())
            {
                return true;
            }
            else
            {
                var result = myContext.Carts.Any(p => p.Products.Type == productToValidate.Type && p.CartID == contextID && p.Applied == true);
                if (result == true)
                {
                    throw new IndexOutOfRangeException("Sorry, you have already applied this voucher");
                }
                else
                {
                    return true;
                }
            }

        }

        public override string GetErrorMessage()
        {
            return errorMessage;
        }

        
    }
}