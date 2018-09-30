using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcweb.Models
{
    public partial class OrderDetail
    {
        public int orderdetail_id;
        public string proName;
        public double proPrice;
        public int proQuantity;
        public double TotalPayment;

    }
}