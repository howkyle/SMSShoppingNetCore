using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; } // number of similar items in the cart
        public double Price { get; set; } //price of total group of similar items
        public Nullable<int> ProductID { get; set; }
        public virtual Product Product { get; set; } //stores the product IDs for item
        public Nullable<int> CartID { get; set; } //stores the id of the associated cart
        public virtual Cart Cart { get; set; } //relationship to the cart

    }
}
