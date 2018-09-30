using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcweb.Models;

namespace mvcweb.Controllers
{
    public class CustomersController : Controller
    {
        private dinhlvEntities db = new dinhlvEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                //Check Email đã đăng ký hay chưa
                using (dinhlvEntities db = new dinhlvEntities())
                {
                    var cusobj = db.Customers.Where(c => c.CusEmail == customer.CusEmail).FirstOrDefault();
                    if (cusobj == null)
                    {
                        customer.Status = true;
                        db.Customers.Add(customer);
                        db.SaveChanges();
                        cusobj = db.Customers.Where(c => c.CusEmail == customer.CusEmail).FirstOrDefault();
                        Session["CusId"] = cusobj.CustomerId;
                        Session["CusName"] = customer.CusName;
                        return RedirectToAction("Index", "Home");
                    }
                }

            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CusName,CusEmail,CusPhone,CusAdress,CusGender,CusBirthday,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //logout
        public ActionResult Logout()
        {
            Session["CusId"] = null;
            Session["CusName"] = null;
            return View("../Home/Index");

        }
        //Dinhlv added
        // GET: Users/DangNhap
        public ActionResult LoginView()
        {
            return View("Login");
        }
        public ActionResult Login(Customer cus)
        {

            using (dinhlvEntities db = new dinhlvEntities())
            {
                var customerdetail = db.Customers.Where(x => x.CusEmail == cus.CusEmail && x.CusPassword == cus.CusPassword).FirstOrDefault();
                if (customerdetail != null)
                {
                    Session["CusId"] = customerdetail.CustomerId;
                    Session["CusName"] = customerdetail.CusName;
                    return Json(new { success = true, responseText = "Đăng nhập thành công" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Đăng nhập không thành công" }, JsonRequestBehavior.AllowGet);
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
