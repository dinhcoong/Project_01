using mvcweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

namespace mvcweb.Controllers
{
    public class HomeController : Controller
    {
        private dinhlvEntities db = new dinhlvEntities();
        public ActionResult Index()
        {
            var lstCate = db.Categories.ToList();
            ViewBag.lstCate = lstCate;
            var products = db.Products.Include(p => p.Category).Take(12);
            return View(products.ToList());

        }
        public ActionResult Search(string searchString, int? page)
        {
            var products = db.Products.Include(p => p.Category);
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProName.Contains(searchString)
                                       || s.ProDesc.Contains(searchString));
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(products.OrderBy(p => p.ProductId).ToPagedList(pageNumber, pageSize));


        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}