using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
    }
}
