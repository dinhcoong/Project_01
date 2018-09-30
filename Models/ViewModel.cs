using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcweb.Models
{
    public class ViewModel
    {

        public IEnumerable<Users> Users { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categorys { get; set; }
        public IEnumerable<Order> Oders { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}