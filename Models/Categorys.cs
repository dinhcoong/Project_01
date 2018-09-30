using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcweb.Models
{
    public partial class Category
    {
    }
        public class ProductOfCate
        {
            public List<Product> ListProduct { get; set; }
        }
}