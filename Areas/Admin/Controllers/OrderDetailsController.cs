using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcweb.Models;

namespace mvcweb.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        private dinhlvEntities db = new dinhlvEntities();

        // GET: Admin/OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            return View(orderDetails.ToList());
        }

        // GET: Admin/OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewModel vmodelObj = new ViewModel();
            List<OrderDetail> lstOderProduct = new List<OrderDetail>();
            lstOderProduct =
           (List<OrderDetail>)(from od in db.OrderDetails
                               join o in db.Orders on od.OrderId equals o.OrderId
                               join p in db.Products on od.ProductId equals p.ProductId
                               where o.OrderId == id
                               select new {orderdetaiid=od.OrderDetailId, proName = p.ProName, Quantity = od.Quantity, proPrice = p.Price }).ToList().Select(op => new OrderDetail { orderdetail_id =op.orderdetaiid,proName = op.proName, proQuantity = op.Quantity, proPrice = op.proPrice,TotalPayment= op.proPrice * op.Quantity }).ToList();

            //var count = lstOderProduct.Count();
            //  vmodelObj.OrderDetails = lstOderProduct;
            if (lstOderProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = id;
            ViewBag.CusName = id;
            return View(lstOderProduct);
        }

        #region khong can thiết
        // GET: Admin/OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderOption");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProName");
            return View();
        }

        // POST: Admin/OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetailId,OrderId,ProductId,Quantity")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderOption", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProName", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderOption", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProName", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetailId,OrderId,ProductId,Quantity")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderOption", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProName", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
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
