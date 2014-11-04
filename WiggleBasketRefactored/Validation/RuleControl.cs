using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiggleBasketRefactored.Validation
{
   
        public delegate bool RuleControl(object toValidate, RuleArgs e);
        public delegate bool RuleControl<T, R>(T toValidate, R e) where R : RuleArgs;

   
}