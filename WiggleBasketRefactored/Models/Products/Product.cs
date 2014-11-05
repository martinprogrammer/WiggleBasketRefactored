using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiggleBasketRefactored.Models.Products
{
    public class Product  
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string ImageName { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public decimal OfferThreshold { get; set; }
    }
}