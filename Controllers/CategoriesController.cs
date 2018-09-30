using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcweb.Models;
using PagedList;

namespace mvcweb.Controllers
{
    public class CategoriesController : Controller
    {
        private dinhlvEntities db = new dinhlvEntities();

        // GET: Categories
        public ActionResult Index()
        {

            return View(db.Categories.ToList());
        }
        //Get by cate// added by dinhlv

        public ActionResult GetbyCate(int? cateid,int?page)
        {
            int pageSize = 16;
            int pageNumber = (page ?? 1);

            
            ViewModel vmodelObj = new ViewModel();
            List<Product> lstProduct = new List<Product>();
            var CateObj = db.Categories.Find(cateid);
            ViewBag.CateId = cateid;
            ViewBag.CateName = CateObj.CateName;

            lstProduct = db.Products.Where(x => x.CategoryId == cateid).ToList();
            return View(lstProduct.OrderBy(p => p.ProductId).ToPagedList(pageNumber, pageSize));
            
            //  vmodelObj.Products = lstProduct.OrderBy(p => p.ProductId).ToPagedList(pageNumber, pageSize);
            //if (CateObj != null)
            //    vmodelObj.Categorys = new List<Category> { CateObj };
            //return View(vmodelObj);



            //var products = from s in db.Products
            //               select s;
            //products = db.Products.Include(p => p.Category);
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    products = products.Where(s => s.ProName.Contains(searchString)
            //                               || s.ProDesc.Contains(searchString));
            //}





        }
        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            LoadCategory();
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            LoadCategory();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // added by dinh

        public ActionResult LoadCategory()
        {
            var listcate = db.Categories
               .Select(t => new SelectListItem
               {
                   Value = t.CategoryId.ToString(),
                   Text = t.CateName
               })
               .ToList();
            listcate.Insert(0, new SelectListItem { Value = "0", Text = "No parent" });
            ViewBag.ParentId = listcate;
            // ViewData["ParentId"] = listcate;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
