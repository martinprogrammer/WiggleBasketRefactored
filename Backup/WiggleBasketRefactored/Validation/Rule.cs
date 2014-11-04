using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WiggleBasketRefactored.Models.Products;
using WiggleBasketRefactored.Models;

namespace WiggleBasketRefactored.Validation
{
    public abstract class Rule: IRule
    {

        public string ruleName { get; set; }
        public string ruleDescription { get; set; }
        public string errorMessage { get; set; }
        public decimal ruleValue { get; set; }
        public Product productToValidate { get; set; }
        public string contextID { get; set; }
        public DBContext myContext { get; set; }

        public abstract bool ExecuteRule();
        public abstract string GetErrorMessage();

      

    }
}