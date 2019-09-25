using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        // GET: Admin/Content
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetByID(id);
            SetViewBag(content.CategoryID);
            return View();
        }

        private void SetViewBag(object categoryID)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Edit(ContentTag model)
        {
            if (ModelState.IsValid)
            {

            }
            SetViewBag(model.ContentID);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ContentTag model)
        {
            if(ModelState.IsValid)
            {

            }
            SetViewBag();
            return View();
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(),"ID","Name", selectedId);

        }
    }
}