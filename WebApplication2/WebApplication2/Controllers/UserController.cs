using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
   
    public class UserController : Controller
    {
        // GET: User

        Model1 db = new Model1();

        public ActionResult Index()
        {
            return View();
        }
        //load trang sử dụng phương thức get để load lên
        public ActionResult Register()
        {
            return View();
        }

        //phương thức post để lấy data về

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                UserManager manager = new UserManager();
                if (manager.checkUserName(registerModel.UserName) && manager.checkUserName(registerModel.Email))
                {
                    user.Email = registerModel.Email;
                    user.UserName = registerModel.UserName;
                    string password = manager.getMD5Hash(registerModel.Password);
                    user.Password = password;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "User");
                }
                ModelState.AddModelError("Rigister", "Tài khoản hoặc mật khẩu không đúng");
                return View();
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel) 
        {
            if (ModelState.IsValid)
            {
                //User user = new User();
                UserManager manager = new UserManager();               
                if (manager.checkUser(loginModel.Password, loginModel.UserName))
                {
                    User user = db.Users.SingleOrDefault(n => n.UserName == loginModel.UserName || n.Email == loginModel.UserName);
                    Session["name"] = loginModel.UserName;
                    Session["userId"] = user.UserID;
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("Login","Tài khoản hoặc mật khẩu không chính xác");
                return View();
            }
            return View();

        }
        public ActionResult Logout()
        {
            Session["name"] = null;
            Session["userId"] = null;
            Session["total"] = 0;
            return RedirectToAction("Index", "Home");
        }
    }
}