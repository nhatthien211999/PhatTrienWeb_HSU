using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Web.Script.Serialization;
namespace WebApplication2.Controllers
{
    public class CartController : Controller
    {

        private const string CartSession = "CartSession";
        Model1 db = new Model1();
        // GET: Cart
        public ActionResult Index()
        {
           
            var cart = Session[CartSession];
            var list = new List<CartItem>();

            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return View(list);
        }

   
        public ActionResult AddItem(int productId, int quatity)
        {
            
            List<Product> products = db.Products.ToList();

            var kq = db.Products.SingleOrDefault(n => n.ProductID == productId);

            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(x=> x.Product.ProductID == productId))
                {
                    foreach (var item in list)
                    {
                        //tăng số lượng SP
                        if(item.Product.ProductID == productId)
                        {
                            item.Quatity += quatity;
                        }
                    }
                }
                else
                {
                    //tạo mới session
                    var item = new CartItem();
                    item.Product = kq;
                    item.Quatity = quatity;

                    list.Add(item);
                }
            }
            else
            {
                //tạo mới session
                var item = new CartItem();
                item.Product = kq;
                item.Quatity = quatity;
                //gán session
                var list = new List<CartItem>();
                list.Add(item);
                
                Session["CartSession"] = list;

            }
            Session["total"] = TongTien();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCart(int productId)
        {
            var cart = Session[CartSession];
            var list = (List<CartItem>)cart;
            if (list.Exists(x => x.Product.ProductID == productId))
            {
                for (int i=0;i<list.Count; i++)
                {
                    if (list[i].Product.ProductID == productId)
                    {
                        list.RemoveAt(i);
                        Session["CartSession"] = list;
                    }
                    
                }
            }
            Session["total"] = TongTien();
            ViewBag.Cart = Session["CartSession"];
            return PartialView("Item");
        }
        [HttpPost]
        public ActionResult DeleteCartAll()
        {
            Session[CartSession] = null;
                   

            ViewBag.Cart = Session[CartSession];
            Session["total"] = 0;
            return PartialView("Item");
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ProductID == item.Product.ProductID);
                if(jsonItem != null)
                {
                    item.Quatity = jsonItem.Quatity;
                }
            }
            Session[CartSession] = sessionCart;
            Session["total"] = TongTien();

            return Json(new 
                {
                status = true
                });
        }
        [HttpPost]
        public ActionResult CheckOut()
        {   
            if(Session["userId"] != null)
            {
                Oder oder = new Oder();
                oder.UserID = Int32.Parse(Session["userId"].ToString());
                oder.CreateDay = DateTime.Now;
                oder.Total = TongTien();
                oder.Status = 0;
                db.Oders.Add(oder);
                db.SaveChanges();


                List<CartItem> cartItem = Session["CartSession"] as List<CartItem>;
                foreach (var item in cartItem)
                {
                    OdersDetail orderDetails = new OdersDetail();
                    orderDetails.OderID = oder.OderID;
                    orderDetails.ProductID = item.Product.ProductID;
                    orderDetails.Quantity = item.Quatity;
                    orderDetails.TotalPrice = item.Quatity * item.Product.Price;
                    db.OdersDetails.Add(orderDetails);
                }
                db.SaveChanges();
                Session["CartSession"] = null;


                return Content("Thanh Toán Thành Công");
            }

            return Content("Bạn cần login trước khi thanh toán đơn hàng");

        }
        public int TongTien()
        {
            List<CartItem> cartItem = Session["CartSession"] as List<CartItem>;
            int tongTien = 0;
            foreach(var item in cartItem)
            {
                tongTien += item.Quatity * Convert.ToInt32(item.Product.Price) ;
            }
            return tongTien;

        }
        
    }
}