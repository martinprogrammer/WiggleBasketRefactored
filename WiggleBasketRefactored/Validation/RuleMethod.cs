using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiggleBasketRefactored.Validation
{
    public class RuleMethod
    {
        private RuleControl myRuleControl;
        private RuleArgs myArgs;
        private string ruleName;

        public RuleMethod(RuleControl control, RuleArgs args)
        {
            myRuleControl = control;
            myArgs = args;
            ruleName = control.Method.Name;
        }

        public bool Invoke(object toValidate)
        {
            return myRuleControl.Invoke(toValidate, myArgs);
        }

    }
}