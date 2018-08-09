using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public double Value { get; set; }
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }
        public virtual List<CartItem> Items { get; set; }
    }
}
