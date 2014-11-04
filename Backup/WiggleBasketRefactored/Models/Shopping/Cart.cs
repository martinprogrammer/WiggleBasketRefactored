using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WiggleBasketRefactored.Models.Products;

namespace WiggleBasketRefactored.Models.Shopping
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordID { get; set; }
        public string CartID { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public bool Applied { get; set; }
        public virtual Product Products { get; set; }

        //public Cart(Cart oldCart)
        //{
        //    OldTotal = oldCart.OldTotal;
        //}

        public Cart()
        {
        }

        //public decimal Total()
        //{
        //    return this.OldTotal + Price * Count;
        //}

    }
}