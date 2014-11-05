using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiggleBasketRefactored.Models.Products
{
    public class ProductSpecific //: IProductSpecifics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string RuleName { get; set; }
        public decimal RuleValue { get; set; }
        public virtual Product Products { get; set; }
    }
}
