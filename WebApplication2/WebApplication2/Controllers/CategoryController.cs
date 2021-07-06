using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryItem(int id)
        {

            List<Product> products = db.Products.ToList();

            var ketqua = (from product in products
                          where product.CategoryID == id
                          select product).ToList();
            return View(ketqua);

        }
    }
}