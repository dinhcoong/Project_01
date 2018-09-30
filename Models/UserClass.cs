using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcweb.Controllers;

namespace mvcweb.Models
{
    public partial class UsersVM
    {
        MHMK obj = new MHMK();

        [Required(ErrorMessage = "Confirmation Password is required.")]
        //[ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("PassWord", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
        public string HashPassword { get { return obj.Encrypt(UserName, PassWord); } }
        public virtual List<Role> RolesOfUser { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        [DataType(DataType.MultilineText)]
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public Nullable<bool> Status { get; set; }


    }
}