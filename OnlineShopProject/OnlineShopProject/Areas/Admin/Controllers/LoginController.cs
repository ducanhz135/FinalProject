using DataAccess.DAO;
using OnlineShopProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopProject.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AdminDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var admin = dao.getByUserName(model.UserName);
                    var adminSession = new AdminLogin();
                    adminSession.UserName = admin.UserName;
                    adminSession.UserID = admin.ID;
                    adminSession.Role = admin.Role;
                    this.Session["AdminId"] = admin.ID;
                    Session.Add(CommonConstants.ADMIN_SESSION, adminSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {

                    ModelState.AddModelError("", "Username doesn't exist!");
                }
                else if (result == -1)
                {

                    ModelState.AddModelError("", "Wrong password!");
                }
                else
                {

                    ModelState.AddModelError("", "Wrong Input!");
                }
            }

            return View("Index");
        }
    }
}