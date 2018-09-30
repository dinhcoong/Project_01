using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcweb.Models;

namespace mvcweb.Controllers
{
    public class CartItemController : Controller
    {
        private dinhlvEntities db = new dinhlvEntities();


        // GET: Users
        public ActionResult Index()
        {
            List<CartItem> giohang = Session["cart"] as List<CartItem>;
            return View(giohang);

        }
        public ActionResult AddToCart(int id)
        {
            string result = "Fail";
            if (Session["cart"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["cart"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> ListItem = Session["cart"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (ListItem.FirstOrDefault(m => m.ProductId == id) == null) // ko co sp nay trong gio hang
            {

                Product sp = db.Products.Find(id);  // tim sp theo sanPhamID
                CartItem newItem = new CartItem()
                {
                    ProductId = id,
                    ProName = sp.ProName,
                    Quantity = 1,
                    Images = sp.ImageLink,
                    Price = double.Parse(sp.Price.ToString())

                };  // Tạo ra 1 CartItem mới
                ListItem.Add(newItem);  // Thêm CartItem vào giỏ 
                result = "Success";
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = ListItem.FirstOrDefault(m => m.ProductId == id);
                cardItem.Quantity++;
                result = "Success";
            }
            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            ViewBag.addcart = "success";
            //return RedirectToAction("Details", "Products", new { id = id });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateQuantity(int id, int quantity)
        {
            // tìm carditem muon sua
            List<CartItem> giohang = Session["cart"] as List<CartItem>;
            CartItem itemSua = giohang.FirstOrDefault(m => m.ProductId == id);
            if (itemSua != null)
            {
                itemSua.Quantity = quantity;
                itemSua.Totalpayment = GetTotalpayment();
                //itemSua.Total //= double.Parse((quantity * itemSua.Price).ToString());
                itemSua.Info = "Cập nhật số lượng thành công";
            }
            return Json(itemSua);

        }

        public double GetTotalpayment()
        {
            double _totalpay = 0;
            try
            {
                List<CartItem> giohang = Session["cart"] as List<CartItem>;
                foreach (var item in giohang)
                {
                    _totalpay += item.Total;
                }
            }
            catch (Exception )
            {
            }
             return _totalpay; 

        }
        public ActionResult DeleteItem(int ProId)
        {
            List<CartItem> giohang = Session["cart"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.ProductId == ProId);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
                itemXoa.Info = "Bạn đã xóa thành công";
                itemXoa.ProductId = ProId;
                itemXoa.Totalpayment = GetTotalpayment();
            }
            return Json(itemXoa);
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
