using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WiggleBasketRefactored.HelperExtensions
{
    public static class Extensions
    {

        public static MvcHtmlString Image(this HtmlHelper helper, string name)
        {
            TagBuilder img = new TagBuilder("img");
            img.Attributes.Add("src", "/content/images/" + name);
            img.Attributes.Add("width", "100");
            return new MvcHtmlString(img.ToString());
        }

    }
}