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
    public class OrdersController : Controller
    {
        private dinhlvEntities db = new dinhlvEntities();

        // GET: Orders
        public ActionResult Index()
        {
            int cusid = int.Parse(Session["CusId"].ToString());
            if (Session["CusId"] != null)
            {
                var cusobj = db.Customers.Find(cusid);
                return View(cusobj);
            }
            return View();
        }
        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            if (Session["CusId"] == null)
            {
                return View();
            }
            else
            {
                int cusid = int.Parse(Session["CusId"].ToString());

                var cusobj = db.Customers.Find(cusid);
                return View(cusobj);

            }
            //  ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CusName");
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                order.OrderOption = "tuy chon";
                order.CustomerId = int.Parse(Session["CusId"].ToString());
                db.Orders.Add(order);
                  db.SaveChanges();
                //getback OrderId
                var NewOrderId = db.Orders.OrderByDescending(x => x.OrderId)
                             .Take(1)
                             .Select(x => x.OrderId)
                             .ToList()
                             .FirstOrDefault();
                List<CartItem> giohang = Session["cart"] as List<CartItem>;
                foreach (var item in giohang)
                {
                    var orderdetail = new OrderDetail();
                    orderdetail.OrderId = NewOrderId;
                    orderdetail.ProductId = item.ProductId;
                    orderdetail.Quantity = item.Quantity;
                    db.OrderDetails.Add(orderdetail);
                    db.SaveChanges();
                }
                Session["cart"] = null;
                return RedirectToAction("Success");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CusName", order.CustomerId);
            return View(order);
        }
        // GET: Tao don hang thanh cong
        public ActionResult Success()
        {
            return View();
        }
        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CusName", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,OrderDate,OrderOption")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CusName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
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
