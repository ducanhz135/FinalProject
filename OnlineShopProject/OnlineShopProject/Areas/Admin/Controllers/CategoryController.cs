using DataAccess.DAO;
using OnlineShopProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopProject.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 1)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);

        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {

                var currentAdmin = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                model.CreatedBy = currentAdmin != null ? currentAdmin.UserID : -1;
                model.MetaTitle = model.MetaTitle.ToLower().Replace(" ", "-");
                var id = new CategoryDao().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Unalbe to create new category");
                }
            }
            SetViewBag();
            return View();
        }


        [HttpDelete]
        public ActionResult Delete(int ID)
        {

            new CategoryDao().Delete(ID);
            return RedirectToAction("Index");
        }


        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.ParentID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
    }
}