using DataAccess.DAO;
using OnlineShopProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopProject.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewProfile(int id)
        {
            var model = new AdminDao().ViewDetail(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Model.EF.Admin admin)
        {
            if (ModelState.IsValid)
            {
                var dao = new AdminDao();

                var encryptedPass = Encryptor.MD5Hash(admin.Password);
                admin.Password = encryptedPass;

                long id = dao.Insert(admin);
                if (id > 0)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to create new user!");
                }

            }
            return View(admin);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new AdminDao().ViewDetail(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Model.EF.Admin admin)
        {
            if (ModelState.IsValid)
            {
                var dao = new AdminDao();
                if (!string.IsNullOrEmpty(admin.Password))
                {
                    var encryptedPass = Encryptor.MD5Hash(admin.Password);
                    admin.Password = encryptedPass;
                }
                var confirm = HttpContext.Request.Params.Get("confirm");
                try
                {
                    var encryptedPass = Encryptor.MD5Hash(confirm);

                    if (admin.Password.Equals(encryptedPass))
                    {
                        var result = dao.Update(admin);
                        if (result)
                        {
                            // SetAlert("Update User successfully", "success");
                            return RedirectToAction("ViewProfile", "Admin");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Unable to update!");
                        }
                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Password does not match!");
                }
            }
            return View(admin);
        }
    }
}