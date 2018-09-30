using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcweb.Models
{
    public class CartItem
    {
        public string Info { get; set; }
        public int ProductId { get; set; }
        public string ProName { get; set; }
        public string Images { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total
        {
            get
            {
                return Quantity * Price;
            }
        }
        public double Totalpayment { get; set; }
      

    }
}