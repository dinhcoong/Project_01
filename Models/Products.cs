using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace mvcweb.Models
{
    public partial class Product
    {
   
        
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
      
    }
}