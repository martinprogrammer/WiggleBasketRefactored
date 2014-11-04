using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WiggleBasketRefactored.Models.Products;
using WiggleBasketRefactored.Models;

namespace WiggleBasketRefactored.Validation
{
    public interface IRule
    {
        string ruleName{get;set;}
        string ruleDescription{get;set;}
        string errorMessage{get;set;}
        decimal ruleValue{get;set;}
        string contextID { get; set; }
        DBContext myContext { get; set; }
        Product productToValidate { get; set; }

        bool ExecuteRule();
        string GetErrorMessage();
    }
}